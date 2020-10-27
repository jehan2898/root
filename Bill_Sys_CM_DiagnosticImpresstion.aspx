<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CM_DiagnosticImpresstion.aspx.cs" Inherits="Bill_Sys_CM_DiagnosticImpresstion"
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
                                <table width="100%" id="tblDiagCode" runat="server">
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 19px">
                                            <b><u>DIAGNOSTIC IMPRESSION</u></b></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%" style="height: 20px">
                                            <asp:CheckBox ID="chk301" runat="server" Text="- (920.0) SCALP CONTUSION" /></td>
                                        <td class="lbl" width="50%" style="height: 20px">
                                            <asp:CheckBox ID="chk302" runat="server" Text="- (784.0) HEADACHES" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk303" runat="server" Text="- (780.4) DIZZINESS" />
                                        </td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk304" runat="server" Text="- (850.0) CONCUSSION WITHOUT LOSS OF CONCIOUSNESS" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk305" runat="server" Text="- (850.1) CONCUSSION WITH BRIEF LOSS OF CONCIOUSNESS" />
                                        </td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk306" runat="server" Text="- (847.0) CERVICAL SPRAIN/STRAIN" /></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk307" runat="server" Text="- (723.4) CERVICAL RADICULITIS" /></td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk308" runat="server" Text="- (722.0) CERVICAL DISC DISPLACEMENT" /></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk309" runat="server" Text="- (847.1) THORACIC SPRAIN/STRAIN" />
                                        </td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk310" runat="server" Text="- (847.2) LUMBAR SPRAIN/STRAIN" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk311" runat="server" Text="- (846.0) LUMBASACRAL SPRAIN/STRAIN" />
                                        </td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk312" runat="server" Text="- (724.4) R/O LUMBAR RADICULITIS" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk313" runat="server" Text="- (722.1) R/O LUMBAR DISC DISPLACEMENT" />
                                        </td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk314" runat="server" Text="- (923.0) RIGHT/LEFT SHOULDER CONTUSION" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk315" runat="server" Text="- (840.9) RIGHT/LEFT SHOULDER SPRAIN/STRAIN" />
                                        </td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk316" runat="server" Text="- (718.31) R/O INTERNAL DERANGEMENT, SHOULDER" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk317" runat="server" Text="- (840.6) R/O TEAR SUPRASPINATUS MUSCLE" />
                                        </td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk318" runat="server" Text="- (924.11) RIGHT/LEFT KNEE CONTUSION" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk319" runat="server" Text="- (844.1 / 844.0 / 844.2) KNEE MCL / LCL/ ACL STRAIN" /></td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk320" runat="server" Text="- (718.36) R/O  KNEE INTERNAL DERANGEMENT" /></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk321" runat="server" Text="- (836.0 / 836.1) R/O MEDIAL / LATERAL MENISCUS TEAR" /></td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk322" runat="server" Text="- (922.1) CONTUSION OF THE CHEST WALL" /></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk323" runat="server" Text="- (845.0) RIGHT / LEFT ANKLE SPRAIN / STRAIN" /></td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk324" runat="server" Text="- (924.21) RIGHT / LEFT ANKLE CONTUSION" /></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk325" runat="server" Text="- (841.9) RIGHT / LEFT ELBOW SPRAIN / STRAIN" /></td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk326" runat="server" Text="- (923.11) RIGHT / LEFT ELBOW CONTUSION" /></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk327" runat="server" Text="- (845.1) RIGHT / LEFT FOOT SPRAIN / STRAIN" /></td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk328" runat="server" Text="- (924.2) RIGHT / LEFT FOOT CONTUSION" /></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk329" runat="server" Text="- (843.9) RIGHT / LEFT HIP / THIGH SPRAIN / STRAIN" /></td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk330" runat="server" Text="- (924.01) RIGHT / LEFT HIP CONTUSION" /></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk331" runat="server" Text="- (842.00) RIGHT / LEFT WRIST SPRAIN / STRAIN" /></td>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk332" runat="server" Text="- (842.1) RIGHT / LEFT HAND SPRAIN / STRAIN" /></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:CheckBox ID="chk333" runat="server" Text="- (736.32) RIGHT / LEFT ELBOW EPICONDYLITIS" /></td>
                                        <td class="lbl" width="50%">
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td class="lbl">
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <b><u>DIAGNOSTIC PLAN AND RECOMMENDATION</u></b>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td align="left" class="lbl" style="height: 15px">
                                            <b><u>DOCTOR’S OPINION</u></b></td>
                                    </tr>
                                    <tr align="center">
                                        <td align="left" class="lbl">
                                            Doctor’S Name
                                            <asp:TextBox ID="TXT_DOCTOR_NAME" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td align="left" class="lbl">
                                            In your opinion, was the incident that the patient described the competent medical
                                            cause of this injury/illness?
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td align="left" class="lbl">
                                            <asp:RadioButtonList ID="RDO_CAUSE_OF_INJURY" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                                <asp:ListItem Value="1">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="height: 18px">
                                        </td>
                                        <td style="height: 18px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="60%">
                                            Are the patient’s complaints consistent with his/her history of the injury/illness?
                                        </td>
                                        <td class="lbl" width="30%">
                                            <asp:RadioButtonList ID="RDO_COMPLAINT_CONSISTENT" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                                <asp:ListItem Value="1">No</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" style="width: 51%">
                                            Is the patient’s history of the injury/illness consistent with your objective findings?
                                        </td>
                                        <td class="lbl" width="40%">
                                            <asp:RadioButtonList ID="RDO_CONSISTENT_OBJ_FIDINGS" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                                <asp:ListItem Value="1">No</asp:ListItem>
                                                <asp:ListItem Value="2">N/A (no findings at this time)</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" style="width: 51%">
                                            What is the percentage (0-100%) of temporary impairment?
                                        </td>
                                        <td class="lbl" width="60%">
                                            <asp:TextBox ID="TXT_PERCENTAGE_TEMP_IMPAIRMENT" runat="server" Text="" Width="90%"></asp:TextBox>
                                            %</td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            Describe findings and relevant diagnostic test results:</td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="100%">
                                            <asp:TextBox ID="TXT_RELAVANT_DIAGNOSTIC_RESULT" TextMode="multiLine" runat="server"
                                                Text="" Width="95%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="100%">
                                            Described prognosis for recovery:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="100%" style="height: 38px">
                                            <asp:TextBox ID="TXT_PROGNOSIS_FOR_RECOVERY" TextMode="multiLine" runat="server"
                                                Text="" Width="95%"></asp:TextBox></td>
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
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%" visible="false">
                    <tr visible="false">
                        <td visible="false" style="height: 22px">
                            <asp:TextBox ID="txtRDO_CAUSE_OF_INJURY" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td visible="false" style="height: 22px">
                            <asp:TextBox ID="txtRDO_COMPLAINT_CONSISTENT" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        
                          <td visible="false" style="height: 22px">
                            <asp:TextBox ID="txtRDO_CONSISTENT_OBJ_FIDINGS" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td style="height: 22px">
                        
                            <asp:TextBox ID="txtEventID" runat="server" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
