<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CM_PhysicalStatus1.aspx.cs" Inherits="Bill_Sys_CM_PhysicalStatus1"
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
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td width="100%" class="TDPart">
                <table width="100%">
                    <tr>
                        <td width="100%" class="TDPart" style="height: 218px">
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td class="lbl" style="height: 20px">
                                        FORWARD MOVMENT -
                                    </td>
                                    <td class="lbl" style="height: 20px">
                                        <asp:CheckBox ID="CHK_CERVICAL_SPINE_FORWARD_MOVEMENT" runat="server" />
                                    </td>
                                    <td class="lbl" style="height: 20px">
                                        BACKWORD MOVMENT -
                                    </td>
                                    <td class="lbl" style="height: 20px">
                                        <asp:CheckBox ID="CHK_CERVICAL_SPINE_BACKWORD_MOVEMENT" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl">
                                        ROTATE HEAD LEFT -
                                    </td>
                                    <td class="lbl">
                                        <asp:CheckBox ID="CHK_CERVICAL_SPINE_ROTATE_HEAD_LEFT" runat="server" />
                                    </td>
                                    <td class="lbl">
                                        ROTATE HEAD RIGHT -
                                    </td>
                                    <td class="lbl">
                                        <asp:CheckBox ID="CHK_CERVICAL_SPINE_ROTATE_HEAD_RIGHT" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl">
                                        BEND NECK LEFT -
                                    </td>
                                    <td class="lbl">
                                        <asp:CheckBox ID="CHK_CERVICAL_SPINE_BEND_NECK_LEFT" runat="server" />
                                    </td>
                                    <td class="lbl">
                                        BEND NECK RIGHT -
                                    </td>
                                    <td class="lbl">
                                        <asp:CheckBox ID="CHK_CERVICAL_SPINE_BEND_NECK_RIGHT" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" >
                                <tr>
                                <%-- <td class="lbl" colspan="4">
                                        
                                        
                                        <asp:RadioButtonList ID="RDO_CERVICAL_SPINE_TENDERNESS" runat="server"  RepeatDirection="Horizontal" style="position: static;">
                                            <asp:ListItem Value="0">Tenderness</asp:ListItem>
                                            <asp:ListItem Value="1">Muscle Spasm To Upper Trapezius And Paraspinal Muscles.</asp:ListItem>
                                            
                                        </asp:RadioButtonList></td>--%>
                                </tr>
                                <tr >
                                    <td  width="25%" > <asp:Label ID="CERVICAL_MUSCLES_APPEAR" runat="Server" Text="CERVICAL MUSCLES APPEAR" Font-Size="Smaller"></asp:Label>
                                    </td>
                                    <td >
                                     <asp:RadioButtonList ID="RDO_CERVICAL_SPINE_MODERATE" runat="server"  RepeatDirection="Horizontal" >
                                            <asp:ListItem Value="0">Moderate</asp:ListItem>
                                            <asp:ListItem Value="1">Severe</asp:ListItem>
                                            
                                        </asp:RadioButtonList>
                                    </td>
                                    <td width="25%"">
                                    <asp:RadioButtonList  id="RDO_CERVICAL_SPINE_WITH" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">With</asp:ListItem>
                                            <asp:ListItem Value="1">Without</asp:ListItem>
                                            
                                        </asp:RadioButtonList>
                                    </td>
                                    <td width="25%">
                                    <asp:RadioButtonList  id="RDO_CERVICAL_SPINE_APPEAR" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Symmetrical</asp:ListItem>
                                            <asp:ListItem Value="1">Assymetrical</asp:ListItem>
                                            
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td >
                                        
                                        <asp:RadioButtonList ID="RDO_CERVICAL_SPINE_TENDERNESS" runat="server"  RepeatDirection="Horizontal" style="position: static;">
                                            <asp:ListItem Value="0">Tenderness</asp:ListItem>
                                            <asp:ListItem Value="1">Muscle Spasm To Upper Trapezius And Paraspinal Muscles.</asp:ListItem>
                                        </asp:RadioButtonList></td>
                                </tr>
                            </table>
                            <table width="80%">
                                <tr>
                                    <td class="lbl" width="10%" >
                                        <asp:Label ID="lbl_LUMBOSACRAL_SPINE" runat="Server" Font-Bold="True" Text="LUMBOSACRAL SPINE"></asp:Label>
                                    </td>
                                    <td class="lbl" width="12%">
                                        NORMAL ROM
                                    </td>
                                    <td class="lbl" width="25%">
                                        PATIENT’S ROM / STRENGTH
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="17%" style="height: 6px">
                                        FLEXION
                                    </td>
                                    <td class="lbl" width="10%" style="height: 6px">
                                        90
                                    </td>
                                    <td class="lbl" width=" 25%" style="height: 6px">
                                        <asp:TextBox ID="TXT_LUMBOSACRAL_SPINE_FLEXION_PATIENT_ROM" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="lbl" width="5%" style="height: 6px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_FLEXION_NORMAL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%" style="height: 6px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_FLEXION_DULL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%" style="height: 6px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_FLEXION_SHARP" Text="S" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%" style="height: 6px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_FLEXION_SPASM" Text="S" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%" style="height: 6px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_FLEXION_INFLAME" Text="I" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="17%" style="height: 3px">
                                        EXTENSION
                                    </td>
                                    <td class="lbl" width="10%" style="height: 3px">
                                        30
                                    </td>
                                    <td class="lbl" width="25%" style="height: 3px">
                                        <asp:TextBox ID="TXT_LUMBOSACRAL_SPINE_EXTENSION_PATIENT_ROM" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="lbl" width="5%" style="height: 3px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_EXTENSION_NORMAL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%" style="height: 3px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_EXTENSION_DULL" Text="D" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%" style="height: 3px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_EXTENSION_SHARP" Text="H" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%" style="height: 3px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_EXTENSION_SPASM" Text="P" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%" style="height: 3px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_EXTENSION_INFLAME" Text="N" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="17%" style="height: 35px">
                                        LEFT ROTATION
                                    </td>
                                    <td class="lbl" width="10%" style="height: 35px">
                                        30
                                    </td>
                                    <td class="lbl" width="25%" style="height: 35px">
                                        <asp:TextBox ID="TXT_LUMBOSACRAL_SPINE_LEFT_ROTATION_PATIENT_ROM" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="lbl" width="5%" style="height: 35px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LEFT_ROTATION_NORMAL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%" style="height: 35px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LEFT_ROTATION_DULL" Text="U" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%" style="height: 35px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LEFT_ROTATION_SHARP" Text="A" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%" style="height: 35px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LEFT_ROTATION_SPASM" Text="A" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%" style="height: 35px">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LEFT_ROTATION_INFLAME" Text="F" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="17%">
                                        RIGHT ROTATION
                                    </td>
                                    <td class="lbl" width="10%">
                                        30
                                    </td>
                                    <td class="lbl" width="25%">
                                        <asp:TextBox ID="TXT_LUMBOSACRAL_SPINE_RIGHT_ROTATION_PATIENT_ROM" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RIGHT_ROTATION_NORMAL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RIGHT_ROTATION_DULL" Text="L" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RIGHT_ROTATION_SHARP" Text="R" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RIGHT_ROTATION_SPASM" Text="S" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RIGHT_ROTATION_INFLAME" Text="L" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="17%">
                                        LT LATERAL FLEXION
                                    </td>
                                    <td class="lbl" width="10%">
                                        20
                                    </td>
                                    <td class="lbl" width="25%">
                                        <asp:TextBox ID="TXT_LUMBOSACRAL_SPINE_LT_LATERAL_FLEXION_PATIENT_ROM" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LT_LATERAL_FLEXION_NORMAL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LT_LATERAL_FLEXION_DULL" Text="L" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LT_LATERAL_FLEXION_SHARP" Text="P" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LT_LATERAL_FLEXION_SPASM" Text="M" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LT_LATERAL_FLEXION_INFLAME" Text="A" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="17%">
                                        RT LATERAL FLEXION
                                    </td>
                                    <td class="lbl" width="10%">
                                        20
                                    </td>
                                    <td class="lbl" width="25%">
                                        <asp:TextBox ID="TXT_LUMBOSACRAL_SPINE_RT_LATERAL_FLEXION_PATIENT_ROM" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RT_LATERAL_FLEXION_NORMAL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RT_LATERAL_FLEXION_DULL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RT_LATERAL_FLEXION_SHARP" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RT_LATERAL_FLEXION_SPASM" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RT_LATERAL_FLEXION_INFLAME" Text="M" runat="server" />
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
                            <table width="100%">
                                <tr>
                                    <td class="lbl" width="20%">
                                        UPPER LUMBAR PAIN
                                    </td>
                                    <td class="lbl">
                                        <asp:RadioButtonList ID="RDO_UPPER_LUMBAR_PAIN" runat="server" Width="90%" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Left</asp:ListItem>
                                            <asp:ListItem Value="1">Right</asp:ListItem>
                                            <asp:ListItem Value="2">Both</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="20%">
                                        LOWER LUMBAR PAIN
                                    </td>
                                    <td class="lbl">
                                        <asp:RadioButtonList ID="RDO_LOWER_LUMBER_PAIN" runat="server" Width="90%" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Left</asp:ListItem>
                                            <asp:ListItem Value="1">Right</asp:ListItem>
                                            <asp:ListItem Value="2">Both</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="20%">
                                        SACRO-ILIAC PAIN
                                    </td>
                                    <td class="lbl">
                                        <asp:RadioButtonList ID="RDO_SACRO_ILLAC_PAIN" runat="server" Width="90%" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Left</asp:ListItem>
                                            <asp:ListItem Value="1">Right</asp:ListItem>
                                            <asp:ListItem Value="2">Both</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="20%">
                                        MUSCLE SPASM
                                    </td>
                                    <td class="lbl">
                                        <asp:RadioButtonList ID="RDO_MUSCLE_SPASM" runat="server" Width="90%" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Left</asp:ListItem>
                                            <asp:ListItem Value="1">Right</asp:ListItem>
                                            <asp:ListItem Value="2">Both</asp:ListItem>
                                        </asp:RadioButtonList>
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
                                    <td class="lbl" width="40%">
                                        <asp:Label ID="lbl_STRAIGHT_LEG_RAISING_TEST_SUPINE" runat="Server" Font-Bold="True"
                                            Text="STRAIGHT LEG RAISING TEST SUPINE"></asp:Label>
                                    </td>
                                    <td class="lbl">
                                        <asp:RadioButtonList ID="RDO_LEG_RAISING_RIGHT" runat="server" Width="90%" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">(+)</asp:ListItem>
                                            <asp:ListItem Value="1">(-)</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="lbl">
                                        RIGHT
                                    </td>
                                    <td class="lbl">
                                        <asp:RadioButtonList ID="RDO_LEG_RAISING_LEFT" runat="server" Width="90%" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">(+)</asp:ListItem>
                                            <asp:ListItem Value="1">(-)</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="lbl">
                                        LEFT
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
                            <table width="70%">
                                <tr>
                                    <td class="lbl" width="10%" style="height: 15px">
                                        HIP
                                    </td>
                                    <td class="lbl" width="10%" style="height: 15px">
                                        NORMAL ROM
                                    </td>
                                    <td class="lbl" width="20%" style="height: 15px">
                                        PATIENT’S ROM / STRENGTH
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="10%">
                                        FLEXION
                                    </td>
                                    <td class="lbl" width="10%">
                                        100
                                    </td>
                                    <td class="lbl" width="20%">
                                        <asp:TextBox ID="TXT_HIP_FLEXION_PATIENT_ROM" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_FLEXION_NORMAL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_FLEXION_DULL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_FLEXION_SHARP" Text="S" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_FLEXION_SPASM" Text="S" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_FLEXION_INFLAME" Text="I" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="10%">
                                        EXTENSION
                                    </td>
                                    <td class="lbl" width="10%">
                                        100
                                    </td>
                                    <td class="lbl" width="20%">
                                        <asp:TextBox ID="TXT_HIP_EXTENSION_PATIENT_ROM" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_EXTENSION_NORMAL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_EXTENSION_DULL" Text="D" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_EXTENSION_SHARP" Text="H" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_EXTENSION_SPASM" Text="P" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_EXTENSION_INFLAME" Text="N" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="10%">
                                        ABDUCTION
                                    </td>
                                    <td class="lbl" width="10%">
                                        40
                                    </td>
                                    <td class="lbl" width="20%">
                                        <asp:TextBox ID="TXT_HIP_ABDUCTION_PATIENT_ROM" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_ABDUCTION_NORMAL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_ABDUCTION_DULL" Text="U" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_ABDUCTION_SHARP" Text="A" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_ABDUCTION_SPASM" Text="A" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_ABDUCTION_INFLAME" Text="F" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="10%">
                                        ADDUCTION
                                    </td>
                                    <td class="lbl" width="10%">
                                        30
                                    </td>
                                    <td class="lbl" width="20%">
                                        <asp:TextBox ID="TXT_HIP_ADDUCTION_PATIENT_ROM" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_ADDUCTION_NORMAL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_ADDUCTION_DULL" Text="L" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_ADDUCTION_SHARP" Text="R" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_ADDUCTION_SPASM" Text="S" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_ADDUCTION_INFLAME" Text="L" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="10%">
                                        INTERNAL ROTATION
                                    </td>
                                    <td class="lbl" width="10%">
                                        40
                                    </td>
                                    <td class="lbl" width="20%">
                                        <asp:TextBox ID="TXT_HIP_INTERNAL_ROTATION_PATIENT_ROM" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_INTERNAL_ROTATION_NORMAL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_INTERNAL_ROTATION_DULL" Text="L" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_INTERNAL_ROTATION_SHARP" Text="P" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_INTERNAL_ROTATION_SPASM" Text="M" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_INTERNAL_ROTATION_INFLAME" Text="A" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="10%">
                                        EXTERNAL ROTATION
                                    </td>
                                    <td class="lbl" width="10%">
                                        40
                                    </td>
                                    <td class="lbl" width="20%">
                                        <asp:TextBox ID="TXT_HIP_EXTERNAL_ROTATION_PATIENT_ROM" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_EXTERNAL_ROTATION_NORMAL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_EXTERNAL_ROTATION_DULL" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_EXTERNAL_ROTATION_SHARP" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_EXTERNAL_ROTATION_SPASM" Text="" runat="server" />
                                    </td>
                                    <td class="lbl" width="5%">
                                        <asp:CheckBox ID="CHK_HIP_EXTERNAL_ROTATION_INFLAME" Text="M" runat="server" />
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
                                    <td class="lbl">
                                        TENDERNESS OVER
                                    </td>
                                    <td class="lbl">
                                        <asp:RadioButtonList ID="RDO_HIP_TENDERNESS" runat="server" Width="90%" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">ANTERIOR / </asp:ListItem>
                                            <asp:ListItem Value="1">LATERAL /</asp:ListItem>
                                            <asp:ListItem Value="2">POSTERIOR ASPECTS.</asp:ListItem>
                                        </asp:RadioButtonList>
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
                        </td>
                    </tr>
                </table>
                <table width="100%" visible="false">
                <tr>
                <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_CERVICAL_SPINE_MODERATE" runat="server"  visible="false"></asp:TextBox>
                </td>
                     <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_CERVICAL_SPINE_WITH" runat="server"  visible="false"></asp:TextBox>
                </td>
                     <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_CERVICAL_SPINE_APPEAR" runat="server"  visible="false"></asp:TextBox>
                </td>
                     <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_CERVICAL_SPINE_TENDERNESS" runat="server"  visible="false"></asp:TextBox>
                </td>
                </tr>
                
                <tr>
                <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_UPPER_LUMBAR_PAIN" runat="server"  visible="false"></asp:TextBox>
                </td>
                     <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_LOWER_LUMBER_PAIN" runat="server"  visible="false"></asp:TextBox>
                </td>
                     <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_SACRO_ILLAC_PAIN" runat="server"  visible="false"></asp:TextBox>
                </td>
                     <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_MUSCLE_SPASM" runat="server"  visible="false"></asp:TextBox>
                </td>
                </tr>
                
                
                <tr>
                <td visible="false">
                <asp:TextBox ID="txtRDO_LEG_RAISING_RIGHT" runat="server" visible="false"></asp:TextBox>
                </td>
                     <td visible="false" >
                <asp:TextBox ID="txtRDO_LEG_RAISING_LEFT" runat="server" visible="false"></asp:TextBox>
                </td>
                     <td visible="false" >
                <asp:TextBox ID="txtRDO_HIP_TENDERNESS" runat="server" visible="false"></asp:TextBox>
                </td>
                     <td visible="false" >
                <asp:TextBox ID="txtEventID" runat="server" visible="false"></asp:TextBox>
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
            </td>
        </tr>
    </table>
</asp:Content>
