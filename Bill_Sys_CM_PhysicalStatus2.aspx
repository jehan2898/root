<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CM_PhysicalStatus2.aspx.cs" Inherits="Bill_Sys_CM_PhysicalStatus2"
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
     
    <table width="100%">
        <tr>
            <td class="lbl" style="width: 25%">
                <b>KNEE</b>
            </td>
            <td class="lbl" width="12%" align="left">
                NORMAL ROM
            </td>
            <td class="lbl" width="25%">
                PATIENT’S ROM / STRENGTH
            </td>
        </tr>
        <tr>
            <td class="lbl" style="width: 25%">
                FLEXION
            </td>
            <td class="lbl" width="10%">
                135</td>
            <td class="lbl" width="10%">
                <asp:TextBox ID="TXT_KNEE_FLEXION_PATIENT_ROM" runat="server"></asp:TextBox>
            </td>
            <td class="lbl" width="10%">
                <asp:CheckBox ID="CHK_KNEE_FLEXION_NORMAL" Text="" runat="server" />
            </td>
            <td class="lbl" width="10%">
                <asp:CheckBox ID="CHK_KNEE_FLEXION_DULL" Text="" runat="server" />
            </td>
            <td class="lbl" width="10%">
                <asp:CheckBox ID="CHK_KNEE_FLEXION_SHARP" runat="server" />
            </td>
            <td class="lbl" width="10%">
                <asp:CheckBox ID="CHK_KNEE_FLEXION_SPASM" runat="server" />
            </td>
            <td class="lbl" width="10%">
                <asp:CheckBox ID="CHK_KNEE_FLEXION_INFLAME" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="lbl" style="width: 25%">
                EXTENSION
            </td>
            <td class="lbl" width="10%">
                0-15</td>
            <td class="lbl" width="10%">
                <asp:TextBox ID="TXT_KNEE_EXTENSION_PATIENT_ROM" runat="server"></asp:TextBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_KNEE_EXTENSION_NORMAL" Text="" runat="server" />
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_KNEE_EXTENSION_DULL" runat="server" />
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_KNEE_EXTENSION_SHARP" runat="server" />
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_KNEE_EXTENSION_SPASM" runat="server" />
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_KNEE_EXTENSION_INFLAME" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td class="lbl" width="20%" style="height: 39px">
                Point Tenderness
            </td>
            <td class="lbl" width="5%" style="height: 39px">
            </td>
            <td class="lbl" colspan="2" style="height: 39px">
                <asp:RadioButtonList ID="RDO_KNEE_POINT_TENDERNESS" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">Postive /</asp:ListItem>
                    <asp:ListItem Value="1">Negative</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%" style="height: 37px">
                Crepitus
            </td>
            <td class="lbl" width="5%" style="height: 37px">
            </td>
            <td class="lbl" style="height: 37px">
                <asp:RadioButtonList ID="RDO_KNEE_CREPITUS" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">Postive /</asp:ListItem>
                    <asp:ListItem Value="1">Negative</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%">
                Effusion
            </td>
            <td class="lbl" width="5%">
            </td>
            <td class="lbl">
                <asp:RadioButtonList ID="RDO_KNEE_EFFUSION" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">Postive /</asp:ListItem>
                    <asp:ListItem Value="1">Negative</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%">
                Joint&nbsp; Line Pain
            </td>
            <td class="lbl" width="5%">
            </td>
            <td class="lbl">
                <asp:RadioButtonList ID="RDO_KNEE_JOINT_LINE_PAIN" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">Postive /</asp:ListItem>
                    <asp:ListItem Value="1">Negative</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%">
                Swelling</td>
            <td class="lbl" width="5%">
            </td>
            <td class="lbl">
                <asp:RadioButtonList ID="RDO_KNEE_SWELLING" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">Postive /</asp:ListItem>
                    <asp:ListItem Value="1">Negative</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="lbl">
            </td>
            <td class="lbl" width="5%">
            </td>
            <td class="lbl">
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td class="lbl" width="20%" style="height: 39px">
                Valgus Test
            </td>
            <td class="lbl" width="5%" style="height: 39px">
            </td>
            <td class="lbl" width="25%" style="height: 39px">
                <asp:RadioButtonList ID="RDO_VALGUS_TEST_RIGHT" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">(+) </asp:ListItem>
                    <asp:ListItem Value="1">(-)-Right</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="lbl" style="height: 39px">
                <asp:RadioButtonList ID="RDO_VALGUS_TEST_LEFT" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">(+) </asp:ListItem>
                    <asp:ListItem Value="1">(-)-Left</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%" style="height: 39px">
                Varus Test
            </td>
            <td class="lbl" width="5%" style="height: 39px">
            </td>
            <td class="lbl" width="25%" style="height: 39px">
                <asp:RadioButtonList ID="RDO_VARUS_TEST_RIGHT" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">(+) </asp:ListItem>
                    <asp:ListItem Value="1">(-)-Right</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="lbl" style="height: 39px">
                <asp:RadioButtonList ID="RDO_VARUS_TEST_LEFT" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">(+) </asp:ListItem>
                    <asp:ListItem Value="1">(-)-Left</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%">
                Mcmurray's Test
            </td>
            <td class="lbl" width="5%">
            </td>
            <td class="lbl" width="25%">
                <asp:RadioButtonList ID="RDO_MCMURRAYS_TEST_RIGHT" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">(+) </asp:ListItem>
                    <asp:ListItem Value="1">(-)-Right</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="lbl">
                <asp:RadioButtonList ID="RDO_MCMURRAYS_TEST_LEFT" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">(+) </asp:ListItem>
                    <asp:ListItem Value="1">(-)-Left</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%">
                Anterior Drawer Sign
            </td>
            <td class="lbl" width="5%">
            </td>
            <td class="lbl" width="25%">
                <asp:RadioButtonList ID="RDO_ANTERIOR_DRAWER_SIGN_RIGHT" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">(+) </asp:ListItem>
                    <asp:ListItem Value="1">(-)-Right</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="lbl">
                <asp:RadioButtonList ID="RDO_ANTERIOR_DRAWER_SIGN_LEFT" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">(+) </asp:ListItem>
                    <asp:ListItem Value="1">(-)-Left</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%">
                Posterior Drawer Sign
            </td>
            <td class="lbl" width="5%">
            </td>
            <td class="lbl" width="25%">
                <asp:RadioButtonList ID="RDO_POSTERIOR_DRAWER_SIGN_RIGHT" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">(+) </asp:ListItem>
                    <asp:ListItem Value="1">(-)-Right</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="lbl">
                <asp:RadioButtonList ID="RDO_POSTERIOR_DRAWER_SIGN_LEFT" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">(+) </asp:ListItem>
                    <asp:ListItem Value="1">(-)-Left</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td class="lbl" colspan="5" width="20%">
                <b>ARM AND HAND</b></td>
        </tr>
        <tr>
            <td class="lbl" width="20%" style="height: 22px">
            </td>
            <td class="lbl" style="height: 22px">
            </td>
            <td class="lbl" style="height: 22px">
                Right &nbsp; &nbsp; Left &nbsp; &nbsp; &nbsp; Both</td>
            <td style="height: 22px">
            </td>
            <td class="lbl" style="height: 22px">
                Right &nbsp; &nbsp; &nbsp;&nbsp; Left &nbsp; &nbsp; &nbsp; &nbsp; Both
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%">
                Pain In Upper Arm
            </td>
            <td width="5%">
            </td>
            <td class="lbl" width="25%">
                <asp:RadioButtonList ID="RDO_PAIN_IN_UPPER_ARM" runat="server" RepeatDirection="Horizontal"
                    Width="60%">
                    <asp:ListItem Value="1"></asp:ListItem>
                    <asp:ListItem Value="0"></asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="lbl">
                Pain In Wrist
            </td>
            <td class="lbl">
                <asp:RadioButtonList ID="RDO_PAIN_IN_WRIST" runat="server" RepeatDirection="Horizontal"
                    Width="60%">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%">
                Pain N Elbow
            </td>
            <td width="5%">
            </td>
            <td class="lbl" width="25%">
                <asp:RadioButtonList ID="RDO_PAIN_N_ELBOW" runat="server" Width="60%" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1"></asp:ListItem>
                    <asp:ListItem Value="0"></asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="lbl">
                Pain In Hand
            </td>
            <td class="lbl">
                <asp:RadioButtonList ID="RDO_PAIN_IN_HAND" runat="server" Width="60%" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%">
                Pain In Forearm
            </td>
            <td width="5%">
            </td>
            <td class="lbl" width="25%">
                <asp:RadioButtonList ID="RDO_PAIN_IN_FOREARM" runat="server" Width="60%" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1"></asp:ListItem>
                    <asp:ListItem Value="0"></asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="lbl">
                Pain & Needless (Hand)
            </td>
            <td class="lbl">
                <asp:RadioButtonList ID="RDO_PAIN_AND_NEEDLESS_HAND" runat="server" Width="60%" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%">
                Pain & Needless (Arm)
            </td>
            <td width="5%">
            </td>
            <td class="lbl" width="25%">
                <asp:RadioButtonList ID="RDO_PAIN_AND_NEEDLESS_ARM" runat="server" Width="60%" RepeatDirection="Horizontal">
                    <asp:ListItem Value="2"></asp:ListItem>
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="lbl">
                Numbness In Hand</td>
            <td class="lbl">
                <asp:RadioButtonList ID="RDO_NUMBNESS_IN_HAND" runat="server" Width="60%" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%" style="height: 45px">
                Pain &amp; Needless(Forearm)</td>
            <td width="5%" style="height: 45px">
            </td>
            <td class="lbl" width="25%" style="height: 45px">
                <asp:RadioButtonList ID="RDO_PAIN_AND_NEEDLESS_FOREARM" runat="server" Width="60%"
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td class="lbl" style="height: 45px">
            </td>
            <td class="lbl" style="height: 45px">
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%" style="height: 45px">
                Numbness In Arm</td>
            <td width="5%" style="height: 45px">
            </td>
            <td class="lbl" width="25%" style="height: 45px">
                <asp:RadioButtonList ID="RDO_NUMBNESS_IN_ARM" runat="server" Width="60%" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td class="lbl" style="height: 45px">
            </td>
            <td class="lbl" style="height: 45px">
            </td>
        </tr>
        <tr>
            <td class="lbl" width="20%">
                Numbness In Forearm</td>
            <td width="5%">
            </td>
            <td class="lbl" width="25%">
                <asp:RadioButtonList ID="RDO_NUMBNESS_IN_FOREARM" runat="server" Width="60%" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="lbl">
            </td>
            <td class="lbl">
            </td>
        </tr>
    </table>
    <%--<table width="70%">
        <tr>
            <td class="lbl" width="10%" style="height: 15px">
                ANKLE
            </td>
            <td class="lbl" width="10%" style="height: 15px">
                NORMAL ROM
            </td>
            <td class="lbl" width="20%" style="height: 15px">
                PATIENT’S ROM / STRENGTH
            </td>
        </tr>
        <tr>
            <td class="lbl" width="10%" style="height: 15px">
                Dorst Flexion
            </td>
            <td class="lbl" width="10%" style="height: 15px">
                35</td>
            <td class="lbl" width="20%" style="height: 15px">
                <asp:TextBox ID="TXT_ANKLE_DORSI_FLEXION_PATIENT_ROM" Text="" runat="server"></asp:TextBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_DORSI_FLEXION_NORMAL" runat="server" Text="" />
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_DORSI_FLEXION_DULL" runat="server" Text=""></asp:CheckBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_DORSI_FLEXION_SHARP" runat="server" Text="S"></asp:CheckBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_DORSI_FLEXION_SPASM" runat="server" Text="S"></asp:CheckBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_DORSI_FLEXION_INFLAME" runat="server" Text="I"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td class="lbl" width="10%" style="height: 15px">
                Plantar Flexion
            </td>
            <td class="lbl" width="10%" style="height: 15px">
                45
            </td>
            <td class="lbl" width="20%" style="height: 15px">
                <asp:TextBox ID="TXT_ANKLE_PLANTAR_FLEXION_PATIENT_ROM" runat="server" Text=""></asp:TextBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_PLANTAR_FLEXION_NORMAL" runat="server" Text=""></asp:CheckBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_PLANTAR_FLEXION_DULL" runat="server" Text="D"></asp:CheckBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_PLANTAR_FLEXION_SHARP" runat="server" Text="H"></asp:CheckBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_PLANTAR_FLEXION_SPASM" runat="server" Text="P"></asp:CheckBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_PLANTAR_FLEXION_INFLAME" runat="server" Text="N"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td class="lbl" width="10%" style="height: 15px">
                INVERSION</td>
            <td class="lbl" width="10%" style="height: 15px">
                15</td>
            <td class="lbl" width="20%" style="height: 15px">
                <asp:TextBox ID="TXT_ANKLE_INVERSION_PATIENT_ROM" runat="server" Text=""></asp:TextBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_INVERSION_NORMAL" runat="server" Text=""></asp:CheckBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_INVERSION_DULL" runat="server" Text="U"></asp:CheckBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_INVERSION_SHARP" runat="server" Text="A"></asp:CheckBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_INVERSION_SPASM" runat="server" Text="A"></asp:CheckBox></td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_INVERSION_INFLAME" runat="server" Text="F"></asp:CheckBox></td>
        </tr>
        <tr>
            <td class="lbl" width="10%" style="height: 15px">
                EVERSION</td>
            <td class="lbl" width="10%" style="height: 15px">
                15</td>
            <td class="lbl" width="20%" style="height: 15px">
                <asp:TextBox ID="TXT_ANKLE_EVERSION_PATIENT_ROM" Text="" runat="server"></asp:TextBox>
            </td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_EVERSION_NORMAL" runat="server" Text=""></asp:CheckBox></td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_EVERSION_DULL" runat="server" Text="L"></asp:CheckBox></td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_EVERSION_SHARP" runat="server" Text="R"></asp:CheckBox></td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_EVERSION_SPASM" runat="server" Text="S"></asp:CheckBox></td>
            <td class="lbl" width="5%">
                <asp:CheckBox ID="CHK_ANKLE_EVERSION_INFLAME" runat="server" Text="L"></asp:CheckBox></td>
        </tr>
    </table>--%>
    <table width="100%">
        <tbody>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td class="lbl" width="20%" colspan="5">
                    <b>FEET</b></td>
            </tr>
            <tr>
                <td class="lbl" width="20%" style="height: 38px">
                </td>
                <td class="lbl" style="height: 38px">
                </td>
                <td class="lbl" style="height: 38px">
                    Right &nbsp; &nbsp; Left &nbsp; &nbsp; &nbsp; Both</td>
                <td style="height: 38px">
                </td>
                <td class="lbl" style="height: 38px">
                </td>
            </tr>
            <tr>
                <td class="lbl" width="20%">
                    <b>ANKLE PAIN</b>
                </td>
                <td width="5%">
                </td>
                <td class="lbl" width="25%">
                    <asp:RadioButtonList ID="rdlFeet_Ankle_Pain" runat="server" Width="60%" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="0">0</asp:ListItem>
                        <asp:ListItem Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="lbl">
                </td>
                <td class="lbl">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="lbl" width="20%">
                    SWOLLEN ANKLE</td>
                <td width="5%">
                </td>
                <td class="lbl" width="25%">
                    <asp:RadioButtonList ID="rdlFeet_Swollen_Ankle" runat="server" Width="60%" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="0">0</asp:ListItem>
                        <asp:ListItem Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="lbl">
                    &nbsp;</td>
                <td class="lbl">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="lbl" width="20%" style="height: 39px">
                    FOOT PAIN
                </td>
                <td width="5%" style="height: 39px">
                </td>
                <td class="lbl" width="25%" style="height: 39px">
                    <asp:RadioButtonList ID="rdlFeet_Foot_Pain" runat="server" Width="60%" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="0">0</asp:ListItem>
                        <asp:ListItem Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="lbl" style="height: 39px">
                </td>
                <td class="lbl" style="height: 39px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="lbl" width="20%" style="height: 39px">
                    NUMBNESS OF FOOT
                </td>
                <td width="5%" style="height: 39px">
                </td>
                <td class="lbl" width="25%" style="height: 39px">
                    <asp:RadioButtonList ID="rdlFeet_Numbness_Of_Foot" runat="server" Width="60%" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="0">0</asp:ListItem>
                        <asp:ListItem Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="lbl" style="height: 39px">
                </td>
                <td class="lbl" style="height: 39px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="lbl" width="20%">
                    SWOLLEN FOOT</td>
                <td width="5%">
                </td>
                <td class="lbl" width="25%">
                    <asp:RadioButtonList ID="rdlFeet_Swollen_Foot" runat="server" Width="60%" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="0">0</asp:ListItem>
                        <asp:ListItem Value="2"></asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="lbl">
                </td>
                <td class="lbl">
                </td>
            </tr>
        </tbody>
    </table>
    <table width="100%">
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="lbl" style="height: 32px">
                <b>GAIT:</b></td>
            <td class="lbl" style="height: 32px">
                <asp:CheckBox ID="chkGait_Normal" runat="server" Text="Normal/"></asp:CheckBox>
            </td>
            <td class="lbl" style="height: 32px">
                <asp:CheckBox ID="chkGait_Slow" runat="server" Text="Slow gait/"></asp:CheckBox>
            </td>
            <td class="lbl" style="height: 32px">
                <asp:CheckBox ID="chkGaint_Antalgic" runat="server" Text="Antalgic /"></asp:CheckBox>
            </td>
            <td class="lbl" width="20%" style="height: 32px">
                <asp:CheckBox ID="chkGaint_Foot_Drop" runat="server" Text="Foot droop (steppage) /">
                </asp:CheckBox>
            </td>
            <td class="lbl" style="height: 32px">
                <asp:CheckBox ID="chkGaint_hemiplegic" runat="server" Text="Hemiplegic."></asp:CheckBox>
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
                    <asp:TextBox ID="txtRDO_KNEE_POINT_TENDERNESS" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                         <asp:TextBox ID="txtRDO_KNEE_CREPITUS" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                    <asp:TextBox ID="txtRDO_KNEE_EFFUSION" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                         <asp:TextBox ID="txtRDO_KNEE_JOINT_LINE_PAIN" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                         <asp:TextBox ID="txtRDO_KNEE_SWELLING" runat="server" Visible="false"></asp:TextBox>
                    </td>
                   
                  <%--  <td>
                    <asp:TextBox ID="txtEventID" runat="server"></asp:TextBox>
                    </td>--%>
                    </tr>
                    <tr visible="false">
                    <td visible="false" style="height: 22px">
                    <asp:TextBox ID="txtRDO_VALGUS_TEST_RIGHT" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                         <asp:TextBox ID="txtRDO_VARUS_TEST_RIGHT" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                    <asp:TextBox ID="txtRDO_MCMURRAYS_TEST_RIGHT" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                         <asp:TextBox ID="txtRDO_ANTERIOR_DRAWER_SIGN_RIGHT" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                         <asp:TextBox ID="txtRDO_POSTERIOR_DRAWER_SIGN_RIGHT" runat="server" Visible="false"></asp:TextBox>
                    </td>
                   
                  <%--  <td>
                    <asp:TextBox ID="txtEventID" runat="server"></asp:TextBox>
                    </td>--%>
                    </tr>
                    
                    
                    
                     <tr visible="false">
                    <td visible="false" style="height: 22px">
                    <asp:TextBox ID="txtRDO_VALGUS_TEST_LEFT" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                         <asp:TextBox ID="txtRDO_VARUS_TEST_LEFT" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                    <asp:TextBox ID="txtRDO_MCMURRAYS_TEST_LEFT" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                         <asp:TextBox ID="txtRDO_ANTERIOR_DRAWER_SIGN_LEFT" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                         <asp:TextBox ID="txtRDO_POSTERIOR_DRAWER_SIGN_LEFT" runat="server" Visible="false"></asp:TextBox>
                    </td>
                   
                  <%--  <td>
                    <asp:TextBox ID="txtEventID" runat="server"></asp:TextBox>
                    </td>--%>
                    </tr>
                    
                    
                          <tr visible="false">
                    <td visible="false" style="height: 23px">
                    <asp:TextBox ID="txtRDO_PAIN_IN_UPPER_ARM" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 23px">
                         <asp:TextBox ID="txtRDO_PAIN_N_ELBOW" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 23px">
                    <asp:TextBox ID="txtRDO_PAIN_IN_FOREARM" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 23px">
                         <asp:TextBox ID="txtRDO_PAIN_AND_NEEDLESS_ARM" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 23px">
                         <asp:TextBox ID="txtRDO_PAIN_AND_NEEDLESS_FOREARM" runat="server" Visible="false"></asp:TextBox>
                    </td>
                   
                  <%--  <td>
                    <asp:TextBox ID="txtEventID" runat="server"></asp:TextBox>
                    </td>--%>
                    </tr>
                    
                    
                    
                       
                          <tr visible="false">
                    <td visible="false" style="height: 21px">
                    <asp:TextBox ID="txtRDO_NUMBNESS_IN_ARM" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 21px">
                         <asp:TextBox ID="txtrdlFeet_Numbness_Of_Foot" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 21px">
                    <asp:TextBox ID="txtRDO_NUMBNESS_IN_FOREARM" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 21px">
                         <asp:TextBox ID="txtRDO_PAIN_IN_WRIST" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 21px">
                         <asp:TextBox ID="txtRDO_PAIN_IN_HAND" runat="server" Visible="false"></asp:TextBox>
                    </td>
                   
                  <%--  <td>
                    <asp:TextBox ID="txtEventID" runat="server"></asp:TextBox>
                    </td>--%>
                    </tr>
                    
                        <tr visible="false">
                    <td visible="false" style="height: 21px">
                    <asp:TextBox ID="txtRDO_PAIN_AND_NEEDLESS_HAND" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 21px">
                         <asp:TextBox ID="txtRDO_NUMBNESS_IN_HAND" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 21px">
                    <asp:TextBox ID="txtrdlFeet_Ankle_Pain" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 21px">
                         <asp:TextBox ID="txtrdlFeet_Swollen_Ankle" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 21px">
                         <asp:TextBox ID="txtrdlFeet_Foot_Pain" runat="server" Visible="false"></asp:TextBox>
                    </td>
                   
                  <%--  <td>
                    <asp:TextBox ID="txtEventID" runat="server"></asp:TextBox>
                    </td>--%>
                    </tr>
                    
                    
                    
                      <tr visible="false">
                    <td visible="false" style="height: 21px">
                    <asp:TextBox ID="txtrdlFeetNumbness_Of_Foot" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 21px">
                         <asp:TextBox ID="txtrdlFeet_Swollen_Foot" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 21px">
                    <asp:TextBox ID="TextBox3" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 21px">
                         <asp:TextBox ID="TextBox4" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <%--<td visible="false" style="height: 21px">
                         <asp:TextBox ID="TextBox5" runat="server" Visible="false"></asp:TextBox>
                    </td>--%>
                   
                    <td>
                    <asp:TextBox ID="txtEventID" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    </tr>
                    </table>
                           </td>
        </tr>
    </table>
    
</asp:Content>
