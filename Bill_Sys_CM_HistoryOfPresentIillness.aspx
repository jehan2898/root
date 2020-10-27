<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CM_HistoryOfPresentIillness.aspx.cs" Inherits="Bill_Sys_CM_HistoryOfPresentIillness"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table class="ContentTable" width="100%">
        <tr>
            <td class="TDPart">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblHeading" runat="server" Text="INITIAL EXAMINATION" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lbl_PATIENT_NAME" runat="server" Text="Patient's Name"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="472px" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lbl_DOA" runat="server" Font-Bold="False" Text="Date Of Accident"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_DOA" runat="server" Width="85px" BackColor="Transparent" BorderColor="Transparent"
                                                BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="LBL_DOE" runat="server" Font-Bold="False" Text="Date Of Examination"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_DOE" runat="server" Width="85px" BackColor="Transparent" BorderColor="Transparent"
                                                BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="LBL_DOB" runat="server" Font-Bold="False" Text="Date Of Birth"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_DOB" runat="server" Width="85px" BackColor="Transparent" BorderColor="Transparent"
                                                BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <%--   <tr>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lbl_PATIENT_NAME" runat="server" Text="Patient's Name"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="472px" Enabled="false"></asp:TextBox></td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lbl_DOA" runat="server" Font-Bold="False" Text="Date Of Accident" ></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_DOA" runat="server" Width="85px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="LBL_DOE" runat="server" Font-Bold="False" Text="Date Of Examination" Enabled="false"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_DOE" runat="server" Width="85px" Enabled="false"></asp:TextBox></td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="LBL_DOB" runat="server" Font-Bold="False" Text="Date Of Birth"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_DOB" runat="server" Width="85px" Enabled="false"></asp:TextBox></td>
                                    </tr>--%>
                                    <tr>
                                        <td colspan="4" align="left">
                                            <asp:Label ID="lbl_History" runat="server" Font-Bold="true" Text="History Of Present Illness:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="left">
                                            <asp:Label ID="lbl_History_injury" runat="server" Text="Based on the patient’s history, where and how did the injury/illness happen:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="left" style="height: 38px">
                                            <asp:TextBox ID="TXT_WHERE_HOW_INJURY_HAPPEN" runat="server" TextMode="MultiLine"
                                                Width="850px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="left">
                                            <asp:Label ID="lbl_How_did" runat="server" Text="How did you learn about the injury/illness (check one)"></asp:Label>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td colspan="4" align="left" style="height: 20px">
                                            <asp:RadioButton ID="RDO_INJURY_FROM_PATIENT" runat="server" GroupName="RDO_INJURY_FROM"
                                                Text="Patient"></asp:RadioButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="left">
                                            <asp:RadioButton ID="RDO_INJURY_FROM_Medical_Records" GroupName="RDO_INJURY_FROM"
                                                runat="server" Text="Medical Records"></asp:RadioButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="1" align="left">
                                            <asp:RadioButton ID="RDO_INJURY_FROM_Other" runat="server" GroupName="RDO_INJURY_FROM"
                                                Text="Other (Specify)"></asp:RadioButton>
                                        </td>
                                        <td colspan="3" align="left">
                                            <asp:TextBox ID="TXT_INJURY_FROM_OTHERS" runat="server" Width="700px"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="lbl" style="width: 61%">
                                            <asp:RadioButtonList ID="RDO_INJURY_FROM" runat="server" Width="90%" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">PATIENT</asp:ListItem>
                                                <asp:ListItem Value="1">MedicalRecords</asp:ListItem>
                                                <asp:ListItem Value="2">OtherSpecify</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td class="lbl" style="width: 44px">
                                            <asp:TextBox ID="txtOtherSpecify" runat="server" Width="95%"></asp:TextBox></td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;<%-- <asp:RadioButton ID="RDO_TREAT_INJURY_Yes" runat="server" GroupName="RDO_TREAT_INJURY"
                                                Text="Yes"></asp:RadioButton>
                                            <asp:RadioButton ID="RDO_TREAT_INJURY_N
                                            o" runat="server" GroupName="RDO_TREAT_INJURY"
                                                Text="No"></asp:RadioButton>--%>&nbsp; &nbsp; &nbsp;&nbsp;
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label5" runat="server" Text="Did another health provider treat this injury/illness including hospitalization and/or surgery ?"></asp:Label></td>
                                        <td align="left" class="lbl" >
                                            <asp:RadioButtonList ID="RDO_TREAT_INJURY" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">YES</asp:ListItem>
                                                <asp:ListItem Value="1">NO</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td align="left" width="30%">
                                            <asp:Label ID="Label1" runat="server" Text="  If yes, give details"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="left">
                                            <asp:TextBox ID="TXT_ANOTHER_HEALTH_PROVIDER_TREATED_DETAILS" runat="server" TextMode="MultiLine"
                                                Width="850px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lbl_previously_treated" runat="server" Text="Have you previously treated this patient for a similar work-related injury/illness?"></asp:Label></td>
                                        <td align="left" class="lbl" style="width: 44px">
                                            <asp:RadioButtonList ID="RDO_PREVIOUSLY_TREATED_No" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">YES</asp:ListItem>
                                                <asp:ListItem Value="1">NO</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblIf_yes_give_details" runat="server" Text="If yes,when"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="left">
                                            <asp:TextBox ID="TXT_PREVIOUS_TREATMENT_WORK_RELATED_INJURY" runat="server" TextMode="MultiLine"
                                                Width="850px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="left">
                                            <asp:Label ID="lbl_TREATMENT" runat="server" Font-Bold="true" Text="Treatment Rendered Today"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="left" class="lbl">
                                            <asp:CheckBox ID="chk101" runat="server" Text="99201 / 99241 – Community Medical Care of N.Y., P.C. OR OTHER OUTPATIENT VISIT / CONSULTATION" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="left" class="lbl">
                                            <asp:CheckBox ID="chk102" runat="server" Text="99202 / 99242 – Community Medical Care of N.Y., P.C. OR OTHER OUTPATIENT VISIT / CONSULTATION" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="left" class="lbl">
                                            <asp:CheckBox ID="chk103" runat="server" Text="99203 / 99243 – Community Medical Care of N.Y., P.C. OR OTHER OUTPATIENT VISIT / CONSULTATION" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="left" class="lbl">
                                            <asp:CheckBox ID="chk104" runat="server" Text="99204 / 99244 – Community Medical Care of N.Y., P.C. OR OTHER OUTPATIENT VISIT / CONSULTATION" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="left" class="lbl">
                                            <asp:CheckBox ID="chk105" runat="server" Text="99205 / 99245 – Community Medical Care of N.Y., P.C. OR OTHER OUTPATIENT VISIT / CONSULTATION" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" visible="false">
                                    <tr visible="false">
                                        <td visible="false" style="height: 22px">
                                            <asp:TextBox runat="server" ID="txtRDO_INJURY_FROM" Visible="false"></asp:TextBox>
                                        </td>
                                        <td visible="false" style="height: 22px">
                                            <asp:TextBox runat="server" ID="txtRDO_TREAT_INJURY" Visible="false"></asp:TextBox>
                                        </td>
                                        <td visible="false" style="height: 22px">
                                            <asp:TextBox runat="server" ID="txtRDO_PREVIOUSLY_TREATED_No" Visible="false"></asp:TextBox>
                                        </td>
                                        <td visible="false" style="height: 22px">
                                            <asp:TextBox runat="server" ID="txtEventID" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td align="center">
                                            <%--<asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click" />--%>
                                            <asp:Button ID="btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="btnSave_Click" />
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
</asp:Content>
