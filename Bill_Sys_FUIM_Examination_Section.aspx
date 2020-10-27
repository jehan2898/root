<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_FUIM_Examination_Section.aspx.cs" Inherits="Bill_Sys_FUIM_Examination_Section" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td width="100%" class="TDPart" style="height: 100%">
          <table width="100%">
              <tr>
                  <td class="lbl" colspan="2">
                      <asp:Label ID="lblError" runat="server" Visible="False" Width="50%"></asp:Label></td>
                  <td class="lbl" style="width: 14%">
                  </td>
                  <td width="25%">
                  </td>
              </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            Patient's Name
                        </td>
                        <td width="40%">
                            <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="90%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="lbl" style="width: 14%">
                            Date of accident
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="TXT_DOA" runat="server" Width="60%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="20%" >
                            DOS</td>
                        <td width="20%">
                            <asp:TextBox ID="TXT_DOS" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                           <%-- <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" / >
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender15" runat="server" TargetControlID="txtDOE"
                                PopupButtonID="imgbtnFromDate" />--%>
                        </td>
                        <td class="lbl" style="width: 14%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;<%-- <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDOB"
                                PopupButtonID="imgbtnFromDate" />--%></td>
                    </tr>
                </table>
                 <table width="100%">
    <tr>
        <td style="width: 20%; height: 21px;" class="lbl">
        PLAIN SCALE RATING:
        </td>
        <td style="width: 20%; height: 21px;" class="lbl">
        0- No Pian  10- Worse Pain 
        </td>
        <td style="width: 60%; height: 21px;" class="lbl">
            <asp:textbox id="TXT_PAIN_SCALE_RATING" runat="server" width="85%"></asp:textbox>
        </td>
    </tr>
</table>
        <table width=100%>
            <tr>
                <td style="width: 20%" class="lbl">
        <asp:Label ID="lbl_Examination" runat="server" Font-Bold="True" Font-Underline="True"
            Text="EXAMINATION:" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%">
                </td>
                <td style="width: 20%" class="lbl">
                </td>
                <td style="width: 9%">
                </td>
                <td style="width: 9%">
                </td>
                <td style="width: 9%">
                </td>
                <td style="width: 9%">
                </td>
                <td style="width: 9%">
                </td>
                <td style="width:15%"></td>
            </tr>
            <tr>
                <td style="width: 20%;" class="lbl">
        <asp:Label ID="lbl_ServicalSpine" runat="server" Text="CERVICAL SPINE" Width="80%" CssClass="lbl" Font-Bold="True" Font-Underline="True"></asp:Label></td>
                <td style="width: 15%;">
                    <asp:Label ID="lbl_NormalRom" runat="server" Text="NORMAL ROM"
            Width="90%" CssClass="lbl"></asp:Label></td>
                <td style="width: 20%;" class="lbl">
        <asp:Label ID="lbl_patientRom" runat="server" Text="PATIENT'S ROM / STRENGTH" CssClass="lbl" Width="97%"></asp:Label></td>
                <td style="width: 9%;">
                </td>
                <td style="width: 9%;">
                </td>
                <td style="width: 9%;">
                </td>
                <td style="width: 9%;">
                </td>
                <td style="width: 9%;">
                </td> <td style="width:15%"></td>
            </tr>
            <tr>
                <td style="width: 20%" class="lbl">
        <asp:Label ID="lbl_Flexion" runat="server" Text="FLEXION" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 15%">
                    <asp:Label
            ID="lbl_60" runat="server" Text="60"></asp:Label></td>
                <td style="width: 20%" class="lbl">
        <asp:TextBox ID="TXT_CERVICAL_SPINE_FLEXION_PATIENT_ROM" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_FLEXION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_FLEXION_DULL" runat="server" Text=" " /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_FLEXION_SHARP" runat="server" Text="S" /></td>
                <td style="width: 10%" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_FLEXION_SPASM" runat="server" Text="S" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_FLEXION_INFLAME" runat="server" Text="I" /></td>
         <td style="width:15%"></td>
            </tr>
            <tr>
                <td style="width: 20%; height: 17px;" class="lbl">
        <asp:Label ID="chk_Extention1" runat="server" Text="EXTENSION" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 15%;">
                    <asp:Label ID="lbl_50"
            runat="server" Text="50"></asp:Label></td>
                <td style="width: 20%; height: 17px;" class="lbl">
        <asp:TextBox ID="TXT_CERVICAL_SPINE_EXTENSION_PATIENT_ROM" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%;">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_EXTENSION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_EXTENSION_DULL" runat="server" Text="D" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_EXTENSION_SHARP" runat="server" Text="H" /></td>
                <td style="width: 10%; height: 17px;" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_EXTENSION_SPASM" runat="server" Text="P" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_EXTENSION_INFLAME" runat="server" Text="N" /></td>
         <td style="width:15%; height: 17px;"></td>
            </tr>
            <tr>
                <td style="width: 20%;" class="lbl">
        <asp:Label ID="lbl_LeftRotation" runat="server" Text="LEFT ROTATION" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 15%;">
                    <asp:Label ID="lbl_80" runat="server" Text="80"></asp:Label></td>
                <td style="width: 20%; height: 22px;" class="lbl">
        <asp:TextBox ID="TXT_CERVICAL_SPINE_LEFT_ROTATION_PATIENT_ROM" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%;">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_LEFT_ROTATION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_LEFT_ROTATION_DULL" runat="server" Text="U" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_LEFT_ROTATION_SHARP" runat="server" Text="A" /></td>
                <td style="width: 10%; height: 22px;" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_LEFT_ROTATION_SPASM" runat="server" Text="A" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_LEFT_ROTATION_INFLAME" runat="server" Text="F" /></td>
         <td style="width:15%; height: 22px;"></td>
            </tr>
            <tr>
                <td style="width: 20%" class="lbl">
        <asp:Label ID="lbl_RightRotation" runat="server" Text="RIGHT ROTATION" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 15%">
        <asp:Label ID="lbl_801" runat="server" Text="80"></asp:Label></td>
                <td style="width: 20%" class="lbl">
        <asp:TextBox ID="TXT_CERVICAL_SPINE_RIGHT_ROTATION_PATIENT_ROM" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_RIGHT_ROTATION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_RIGHT_ROTATION_DULL" runat="server" Text="L" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_RIGHT_ROTATION_SHARP" runat="server" Text="R" /></td>
                <td style="width: 10%" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_RIGHT_ROTATION_SPASM" runat="server" Text="S" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_RIGHT_ROTATION_INFLAME" runat="server" Text="L" /></td>
         <td style="width:15%"></td>
            </tr>
            <tr>
                <td style="width: 20%" class="lbl">
        <asp:Label ID="lbl_LiLateralFlextion" runat="server" Text="LT LATERAL FLEXION" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 15%">
        <asp:Label ID="lbl_40" runat="server" Text="40"></asp:Label></td>
                <td style="width: 20%" class="lbl">
        <asp:TextBox ID="TXT_CERVICAL_SPINE_LT_LATERAL_FLEXION_PATIENT_ROM" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_LT_LATERAL_FLEXION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPIN_LT_LATERAL_FLEXION_DULL" runat="server" Text="L" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_LT_LATERAL_FLEXION_SHARP" runat="server" Text="P" /></td>
                <td style="width: 10%" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_LT_LATERAL_FLEXION_SPASM" runat="server" Text="M" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_LT_LATERAL_FLEXION_INFLAM" runat="server" Text="A" /></td>
         <td style="width:15%"></td>
            </tr>
            <tr>
                <td style="width: 20%;" class="lbl">
        <asp:Label ID="lbl_RtLateralFlextion" runat="server" Text="RT LATERAL FLEXION" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 15%;">
        <asp:Label ID="lbl_401" runat="server" Text="40"></asp:Label></td>
                <td style="width: 20%; height: 24px;" class="lbl">
        <asp:TextBox ID="TXT_CERVICAL_SPINE_RT_LATERAL_FLEXION_PATIENT_ROM" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%;">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_RT_LATERAL_FLEXION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%;">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_RT_LATERAL_FLEXION_DULL" runat="server" Text=" " /></td>
                <td style="width: 9%;">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_RT_LATERAL_FLEXION_SHARP" runat="server" Text=" " /></td>
                <td style="width: 10%; height: 24px;">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_RT_LATERAL_FLEXION_SPASM" runat="server" Text=" " /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_CERVICAL_SPINE_RT_LATERAL_FLEXION_INFLAME" runat="server" Text="M" /></td>
         <td style="width:15%"></td>
       
            </tr>
        </table>
        
           
                              <table width=100%>
                            <tr>
                                <td style="width: 25%; height: 33px;">
        <asp:Label ID="lbl_CervicalMusclesAppere" runat="server" Text="CERVICAL MUSCLES APPEAR" CssClass="lbl" Width="90%"></asp:Label></td>
                                <td style="width: 25%; height: 33px;" colspan="2" class="lbl">
                                    <asp:RadioButtonList ID="RDO_CERVICAL_SPINE_APPEAR" runat="server" RepeatDirection="Horizontal"
                                        Width="25%" Height="12px">
                                        <asp:ListItem Value="0">SYMMRTRICAL</asp:ListItem>
                                        <asp:ListItem Value="1">ASSYMMRTRICAL</asp:ListItem>
                                    </asp:RadioButtonList>
                                    </td>
                                <td style="WIDTH:20%; height: 33px;" colspan="2" class="lbl">
                                    <asp:RadioButtonList ID="RDO_CERVICAL_SPINE_WITH" runat="server" RepeatDirection="Horizontal" Width="15%">
                                        <asp:ListItem Value="0">WITH</asp:ListItem>
                                        <asp:ListItem Value="1">WITHOUT</asp:ListItem>
                                    </asp:RadioButtonList>
                                    </td>
                                <td colspan="2" style="width:30%; height: 33px;" class="lbl">
                                    <asp:RadioButtonList ID="RDO_CERVICAL_SPINE_MODERATE" runat="server" RepeatDirection="Horizontal" Width="60%">
                                        <asp:ListItem Value="0">MODERATE</asp:ListItem>
                                        <asp:ListItem Value="1">SEVERE</asp:ListItem>
                                    </asp:RadioButtonList>
                                    </td>
                            </tr>
                            <tr>
                                <td style="height: 36px; width: 100%;" colspan="7" class="lbl">
                                    &nbsp;<asp:RadioButtonList ID="RDO_CERVICAL_SPINE_TENDERNESS" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0">TENDERNESS /</asp:ListItem>
                                        <asp:ListItem Value="1">MUSCLE SPASM TO UPPER TRAPEZIUS  AND PARASPINAL MUSCLES.</asp:ListItem>
                                    </asp:RadioButtonList>
                                    </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="WIDTH:25%; height: 22px;" class="lbl">
                                    <asp:RadioButtonList ID="RDO_FORAMINAL_COMPRESSION_SPURLING_SIGN" runat="server"
                                        RepeatDirection="Horizontal" Width="99%" Height="22px">
                                        <asp:ListItem Value="0">FORAMINAL COMPRESSION /</asp:ListItem>
                                        <asp:ListItem Value="1">SPURLING SIGN      (</asp:ListItem>
                                    </asp:RadioButtonList>
                                    </td>
                                <td colspan="3" style="width: 25%; height: 22px;" class="lbl">
                                    <asp:RadioButtonList ID="RDO_FORAMINAL_COMPRESSION_SPURLING_SIGN_POSITIVE" runat="server"
                                        RepeatDirection="Horizontal" Width="50%">
                                        <asp:ListItem Value="0">POSITIVE /</asp:ListItem>
                                        <asp:ListItem Value="1">NEGATIVE).</asp:ListItem>
                                    </asp:RadioButtonList>
                                    </td>
                                <td style="width: 50%; height: 22px;">
                                </td>
                                 
                            </tr>
                        </table>
                                <table width="100%">
            <tr>
                <td style="width: 20%" class="lbl">
        <asp:Label ID="lbl_Lumbosacral" runat="server" Text="LUMBOSACRAL SPINE" Width="83%" CssClass="lbl" Font-Bold="True" Font-Underline="False"></asp:Label></td>
                <td style="width: 15%">
                    <asp:Label
            ID="lbl_NarmalRom1" runat="server" Text="NORMAL ROM" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 20%" class="lbl">
        <asp:Label ID="lbl_patientRomStrength1" runat="server" Text="PATIENT'S ROM / STRENGTH" Width="100%" CssClass="lbl"></asp:Label></td>
                <td style="width: 9%">
                </td>
                <td style="width: 9%">
                </td>
                <td style="width: 9%">
                </td>
                <td style="width: 9%">
                </td>
                <td style="width: 9%">
                </td>
               
                 
            </tr>
            <tr>
                <td style="width: 20%;" class="lbl">
        <asp:Label ID="lbl_Flexion1" runat="server" Text="FLEXION" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 15%">
                    <asp:Label
            ID="lbl_90" runat="server" Text="90" Width="15%"></asp:Label></td>
                <td style="width: 20%">
        <asp:TextBox ID="TXT_LUMBOSACRAL_SPINE_FLEXION_PATIENT_ROM" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%" >
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_FLEXION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_FLEXION_DULL" runat="server" Text=" " /></td>
                <td style="width:9%" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_FLEXION_SHARP" runat="server" Text="S" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_FLEXION_SPASM" runat="server" Text="S" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_FLEXION_INFLAME" runat="server" Text="I" /></td>
        
            </tr>
            <tr>
                <td style="width: 20%;">
        <asp:Label ID="lbl_Extention1" runat="server" Text="EXTENSION" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 15%;">
                    <asp:Label ID="lbl_30"
            runat="server" Text="30" Width="15%"></asp:Label></td>
                <td style="width: 20%;">
        <asp:TextBox ID="TXT_LUMBOSACRAL_SPINE_EXTENSION_PATIENT_ROM" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%;">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_EXTENSION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_EXTENSION_DULL" runat="server" Text="D" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_EXTENSION_SHARP" runat="server" Text="H" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_EXTENSION_SPASM" runat="server" Text="P" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_EXTENSION_INFLAME" runat="server" Text="N" /></td>
         
            </tr>
            <tr>
                <td style="width: 20%;">
        <asp:Label ID="lbl_LeftRotation1" runat="server" Text="LEFT ROTATION" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 15%">
                    <asp:Label ID="lbl_20" runat="server" Text="20" Width="15%"></asp:Label></td>
                <td style="width: 20%">
        <asp:TextBox ID="TXT_LUMBOSACRAL_SPINE_LEFT_ROTATION_PATIENT_ROM" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LEFT_ROTATION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LEFT_ROTATION_DULL" runat="server" Text="U" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LEFT_ROTATION_SHARP" runat="server" Text="A" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LEFT_ROTATION_SPASM" runat="server" Text="A" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LEFT_ROTATION_INFLAME" runat="server" Text="F" /></td>
         
            </tr>
            <tr>
                <td style="width: 20%;">
        <asp:Label ID="lbl_RightRotation1" runat="server" Text="RIGHT ROTATION" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 15%">
        <asp:Label ID="lbl_20_1" runat="server" Text="20" Width="15%"></asp:Label></td>
                <td style="width: 20%">
        <asp:TextBox ID="TXT_LUMBOSACRAL_SPINE_RIGHT_ROTATION_PATIENT_ROM" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RIGHT_ROTATION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RIGHT_ROTATION_DULL" runat="server" Text="L" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RIGHT_ROTATION_SHARP" runat="server" Text="R" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RIGHT_ROTATION_SPASM" runat="server" Text="S" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RIGHT_ROTATION_INFLAME" runat="server" Text="L" /></td>
         
            </tr>
            <tr>
                <td style="width: 20%;">
        <asp:Label ID="lbl_LTLateralFlextion" runat="server" Text="LT LATERAL FLEXION" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 15%;">
        <asp:Label ID="lbl_30_2" runat="server" Text="30" Width="15%"></asp:Label></td>
                <td style="width: 20%;">
        <asp:TextBox ID="TXT_LUMBOSACRAL_SPINE_LT_LATERAL_FLEXION_PATIENT_ROM" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%;">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LT_LATERAL_FLEXION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LT_LATERAL_FLEXION_DULL" runat="server" Text="L" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LT_LATERAL_FLEXION_SHARP" runat="server" Text="P" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LT_LATERAL_FLEXION_SPASM" runat="server" Text="M" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_LT_LATERAL_FLEXION_INFLAME" runat="server" Text="A" /></td>
          
            </tr>
            <tr>
                <td style="width: 20%;">
        <asp:Label ID="lbl_RTlateralFlexion1" runat="server" Text="RT LATERAL FLEXION" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 15%">
        <asp:Label ID="lbl_30_3" runat="server" Text="30" Width="15%"></asp:Label></td>
                <td style="width: 20%">
        <asp:TextBox ID="TXT_LUMBOSACRAL_SPINE_RT_LATERAL_FLEXION" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RT_LATERAL_FLEXION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RT_LATERAL_FLEXION_DULL" runat="server" Text=" " /></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RT_LATERAL_FLEXION_SHARP" runat="server" Text=" " /></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RT_LATERAL_FLEXION_SPASM" runat="server" Text=" " /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_LUMBOSACRAL_SPINE_RT_LATERAL_FLEXION_INFLAME" runat="server" Text="M" /></td>
        
            </tr>
        </table>
        
                 <table width="100%">
            <tr>
                <td style="width: 40%;">
        <asp:Label ID="lbl_StraightLeg" runat="server" Text="STRAIGHT LEG RAISING TEST SUPINE:" Width="70%" CssClass="lbl"></asp:Label></td>
                <td style="width: 10%;">
        <asp:Label ID="lbl_Right" runat="server" Text="RIGHT :" Width="35%"></asp:Label></td>
                <td colspan="3" class="lbl" style="width: 50%;">
                    <asp:RadioButtonList ID="RDO_STRAIGHT_LEG_RAISING_TEST_SUPINE_RIGHT" runat="server"
                        RepeatDirection="Horizontal" Width="40%">
                        <asp:ListItem Value="0">POSITIVE /</asp:ListItem>
                        <asp:ListItem Value="1">NEGATIVE /</asp:ListItem>
                        <asp:ListItem Value="2">BILATERAL</asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
            </tr>
            <tr>
                <td style="width: 40%">
                </td>
                <td style="width: 10%">
        <asp:Label ID="lbl_left" runat="server" Text="LEFT :" Width="35%"></asp:Label></td>
                <td colspan="3" class="lbl" style="width: 50%">
                    <asp:RadioButtonList ID="RDO_STRAIGHT_LEG_RAISING_TEST_SUPINE_LEFT" runat="server"
                        RepeatDirection="Horizontal" Width="40%">
                        <asp:ListItem Value="0">POSITIVE /</asp:ListItem>
                        <asp:ListItem Value="1">NEGATIVE /</asp:ListItem>
                        <asp:ListItem Value="2">BILATERAL</asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
            </tr>
        </table>
        
                <table width=100%>
            <tr>
                <td style="width: 20%">
        <asp:Label ID="lbl_Shoulder" runat="server" Text="SHOULDER" Width="80%" CssClass="lbl" Font-Bold="True" Font-Underline="True"></asp:Label></td>
                <td style="width: 15%">
                    <asp:Label
            ID="lbl_normalForm2" runat="server" Text="NORMAL ROM" Width="80%" CssClass="lbl"></asp:Label></td>
                <td class="lbl" colspan="2" style="width: 20%">
        <asp:Label ID="Label3" runat="server" Text="PATIENT'S ROM / STRENGTH" CssClass="lbl" Width="100%"></asp:Label></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_RIGHT" runat="server" Text="- RIGHT" Width="80%" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_LEFT" runat="server" Text="- LEFT" Width="80%" /></td>
                <td style="width: 9%">
                </td>
                <td style="width: 9%">
                </td>
            </tr>
            <tr>
                <td style="width: 20%;">
                </td>
                <td style="width: 15%;">
                </td>
                <td style="width: 10%; height: 30px;">
        <asp:Label ID="lbl_Right2" runat="server" Text="RIGHT" CssClass="lbl" Width="50px"></asp:Label></td>
                <td style="width: 10%; height: 30px;">
                    <asp:Label
            ID="lbl_Left3" runat="server" Text="LEFT" CssClass="lbl" Width="42px"></asp:Label></td>
                <td style="width: 9%;">
                </td>
                <td style="width: 10%;">
                </td>
                <td style="width: 8%; height: 26px;">
                </td>
                <td style="width: 9%;">
                </td>
                <td style="width: 8%; height: 26px;">
                </td>
            </tr>
            <tr>
                <td style="width: 20%;">
        <asp:Label ID="lbl_Flexion3" runat="server" Text="FLEXION" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%;">
                    <asp:Label
            ID="LBL_150" runat="server" Text="150" Width="15%"></asp:Label></td>
                <td style="width: 10%;">
        <asp:TextBox ID="TXT_SHOULDER_FLEXION_PATIENT_ROM_RIGHT" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 10%;">
                    <asp:TextBox ID="TXT_SHOULDER_FLEXION_PATIENT_ROM_LEFT" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%;">
        <asp:CheckBox ID="CHK_SHOULDER_FLEXION_NORMAL" runat="server" Text=" " Width="15%" /></td>
                <td style="width: 9%;">
        <asp:CheckBox ID="CHK_SHOULDER_FLEXION_DULL" runat="server" Text=" " /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_FLEXION_SHARP" runat="server" Text="S" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_FLEXION_SPASM" runat="server" Text="S" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_FLEXION_INFLAME" runat="server" Text="I" /></td>
            </tr>
            <tr>
                <td style="width: 20%">
        <asp:Label ID="lbl_Extension3" runat="server" Text="EXTENSION" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%">
                    <asp:Label ID="LBL_150_1"
            runat="server" Text="150" Width="15%"></asp:Label></td>
                <td style="width: 10%">
        <asp:TextBox ID="TXT_SHOULDER_EXTENSION_PATIENT_ROM_RIGHT" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 10%">
                    <asp:TextBox ID="TXT_SHOULDER_EXTENSION_PATIENT_ROM_LEFT" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_SHOULDER_EXTENSION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_EXTENSION_DULL" runat="server" Text="D" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_EXTENSION_SHARP" runat="server" Text="H" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_EXTENSION_SPASM" runat="server" Text="P" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_EXTENSION_INFLAME" runat="server" Text="N" /></td>
            </tr>
            <tr>
                <td style="width: 20%">
        <asp:Label ID="lbl_Abduction" runat="server" Text="ABDUCTION" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%">
        <asp:Label ID="LBL_150_2" runat="server" Text="150" Width="15%"></asp:Label></td>
                <td style="width: 10%">
        <asp:TextBox ID="TXT_SHOULDER_ABDUCTION_PATIENT_ROM_RIGHT" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 10%">
        <asp:TextBox ID="TXT_SHOULDER_ABDUCTION_PATIENT_ROM_LEFT" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_SHOULDER_ABDUCTION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_ABDUCTION_DULL" runat="server" Text="U" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_ABDUCTION_SHARP" runat="server" Text="A" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_ABDUCTION_SPASM" runat="server" Text="A" /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_ABDUCTION_INFLAME" runat="server" Text="F" /></td>
            </tr>
            <tr>
                <td style="width: 20%;">
        <asp:Label ID="lbl_Adduction" runat="server" Text="ADDUCTION" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%;">
        <asp:Label ID="LBL_30_4" runat="server" Text="30" Width="15%"></asp:Label></td>
                <td style="width: 10%;">
        <asp:TextBox ID="TXT_SHOULDER_ADDUCTION_PATIENT_ROM_RIGHT" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 10%;">
        <asp:TextBox ID="TXT_SHOULDER_ADDUCTION_PATIENT_ROM_LEFT" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%;">
        <asp:CheckBox ID="CHK_SHOULDER_ADDUCTION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_ADDUCTION_DULL" runat="server" Text="L" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_ADDUCTION_SHARP" runat="server" Text="R" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_ADDUCTION_SPASM" runat="server" Text="S" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_ADDUCTION_INFLAME" runat="server" Text="L" Width="30px" /></td>
            </tr>
            <tr>
                <td style="width: 20%;">
        <asp:Label ID="lbl_internalRotation" runat="server" Text="INTERNAL ROTATION" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%;">
        <asp:Label ID="lbl_40_5" runat="server" Text="40" Width="15%"></asp:Label></td>
                <td style="width: 10%;">
                    <asp:TextBox ID="TXT_SHOULDER_INTERNAL_ROTATION_PATIENT_ROM_RIGHT" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 10%;">
        <asp:TextBox ID="TXT_SHOULDER_INTERNAL_ROTATION_PATIENT_ROM_LEFT" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%;">
        <asp:CheckBox ID="CHK_SHOULDER_INTERNAL_ROTATION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_INTERNAL_ROTATION_DULL" runat="server" Text="L" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_INTERNAL_ROTATION_SHARP" runat="server" Text="P" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_INTERNAL_ROTATION_SPASM" runat="server" Text="M" /></td>
                <td style="width: 9%;" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_INTERNAL_ROTATION_INFLAME" runat="server" Text="A" /></td>
            </tr>
            <tr>
                <td style="width: 20%">
        <asp:Label ID="lbl_ExternalRotation" runat="server" Text="EXTERNAL ROTATION" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%">
        <asp:Label ID="lbl_40_6" runat="server" Text="40" Width="15%"></asp:Label></td>
                <td style="width: 10%">
        <asp:TextBox ID="TXT_SHOULDER_EXTERNAL_ROTATION_PATIENT_ROM_RIGHT" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 10%">
        <asp:TextBox ID="TXT_SHOULDER_EXTERNAL_ROTATION_PATIENT_ROM_LEFT" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 9%">
                    <asp:CheckBox ID="CHK_SHOULDER_EXTERNAL_ROTATION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_SHOULDER_EXTERNAL_ROTATION_DULL" runat="server" Text=" " /></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_SHOULDER_EXTERNAL_ROTATION_SHARP" runat="server" Text=" " /></td>
                <td style="width: 9%">
        <asp:CheckBox ID="CHK_SHOULDER_EXTERNAL_ROTATION_SPASM" runat="server" Text=" " /></td>
                <td style="width: 9%" class="lbl">
        <asp:CheckBox ID="CHK_SHOULDER_EXTERNAL_ROTATION_INFLAME" runat="server" Text="M" /></td>
            </tr>
                    <tr>
                        <td style="width: 20%">
                        </td>
                        <td style="width: 15%">
                        </td>
                        <td style="width: 10%">
                        </td>
                        <td style="width: 10%">
                        </td>
                        <td style="width: 9%">
                        </td>
                        <td style="width: 9%">
                        </td>
                        <td style="width: 9%">
                        </td>
                        <td style="width: 9%">
                        </td>
                        <td class="lbl" style="width: 9%">
                        </td>
                    </tr>
        </table>
        
         <table width="100%">
            <tr>
                <td style="width: 15%">
        <asp:Label ID="lbl_Hip" runat="server" Text="HIP" Width="80%" CssClass="lbl" Font-Bold="True" Font-Underline="True"></asp:Label></td>
                <td style="width: 15%">
                    <asp:Label
            ID="lbl_NormalRom5" runat="server" Text="NORMAL ROM" Width="80%" CssClass="lbl"></asp:Label></td>
                <td colspan="2" WIDTH="15%" style="width: 70%">
        <asp:Label ID="lbl_PatientRomStrength5" runat="server" Text="PATIENT'S ROM / STRENGTH" Width="80%" CssClass="lbl"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 15%;">
                </td>
                <td style="width: 15%;">
                </td>
                <td style="width: 20%; height: 10px;">
        <asp:Label ID="lbl_patientRomRight" runat="server" Text="RIGHT" CssClass="lbl" Width="25%"></asp:Label></td>
                <td style="width: 50%; height: 10px;">
                    <asp:Label
            ID="lbl_patientRomLeft" runat="server" Text="LEFT" CssClass="lbl" Width="25%"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 15%; height: 22px;">
        <asp:Label ID="lbl_HipFlexion" runat="server" Text="FLEXION" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%; height: 22px;" class="lbl">
                    <asp:Label
            ID="lbl_100" runat="server" Text="100"></asp:Label></td>
                <td style="width: 20%; height: 22px;">
                    <asp:TextBox ID="TXT_HIP_FLEXION_RIGHT" runat="server" Width="50%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 50%; height: 10px;">
                    <asp:TextBox ID="TXT_HIP_FLEXION_LEFT" runat="server" Width="20%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 15%; height: 22px">
        <asp:Label ID="lbl_hipExtension" runat="server" Text="EXTENSION" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%; height: 22px" class="lbl">
                    <asp:Label ID="lbl_100_1"
            runat="server" Text="100"></asp:Label></td>
                <td style="width: 20%; height: 22px">
        <asp:TextBox ID="TXT_HIP_EXTENSION_RIGHT" runat="server" Width="50%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 50%; height: 10px">
                    <asp:TextBox ID="TXT_HIP_EXTENSION_LEFT" runat="server" Width="20%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 15%;">
        <asp:Label ID="lbl_HipAbduction" runat="server" Text="ABDUCTION" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%;" class="lbl">
        <asp:Label ID="lbl_40_7" runat="server" Text="40"></asp:Label></td>
                <td style="width: 20%; height: 10px">
        <asp:TextBox ID="TXT_HIP_ABDUCTION_RIGHT" runat="server" Width="50%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 50%; height: 10px">
        <asp:TextBox ID="TXT_HIP_ABDUCTION_LEFT" runat="server" Width="20%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 15%;">
        <asp:Label ID="lbl_hipAdduction" runat="server" Text="ADDUCTION" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%;" class="lbl">
        <asp:Label ID="lbl_30_7" runat="server" Text="30"></asp:Label></td>
                <td style="width: 20%; height: 22px;">
                    <asp:TextBox ID="TXT_HIP_ADDUCTION_RIGHT" runat="server" Width="50%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 50%; height: 10px;">
        <asp:TextBox ID="TXT_HIP_ADDUCTION_LEFT" runat="server" Width="20%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 15%">
        <asp:Label ID="lbl_hipinternalRotation" runat="server" Text="INTERNAL ROTATION" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%" class="lbl">
        <asp:Label ID="lbl_40_8" runat="server" Text="40"></asp:Label></td>
                <td style="width: 20%; height: 22px;">
        <asp:TextBox ID="TXT_HIP_INTERNAL_ROTATION_RIGHT" runat="server" Width="50%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 50%; height: 10px;">
        <asp:TextBox ID="TXT_HIP_INTERNAL_ROTATION_PATIENT_ROM_LEFT" runat="server" Width="20%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 15%;">
        <asp:Label ID="lbl_HipExternalRotation" runat="server" Text="EXTERNAL ROTATION" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%;" class="lbl">
        <asp:Label ID="lbl_40_9" runat="server" Text="40"></asp:Label></td>
                <td style="width: 20%; height: 22px">
        <asp:TextBox ID="TXT_HIP_EXTERNAL_ROTATION_PATIENT_ROM_RIGHT" runat="server" Width="50%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 50%; height: 10px">
        <asp:TextBox ID="TXT_HIP_EXTERNAL_ROTATION_PATIENT_ROM_LEFT" runat="server" Width="20%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
             <tr>
                 <td style="width: 15%">
                 </td>
                 <td class="lbl" style="width: 15%">
                 </td>
                 <td style="width: 20%; height: 22px">
                 </td>
                 <td style="width: 50%; height: 10px">
                 </td>
             </tr>
        </table>
        
                <table width="100%">
            <tr>
                <td style="width: 15%; height: 16px;">
        <asp:Label ID="lbl_Knee" runat="server" Text="KNEE" Width="90%" CssClass="lbl" Font-Bold="True" Font-Underline="True"></asp:Label></td>
                <td style="width: 15%; height: 16px;">
                    <asp:Label
            ID="lbl_kneeNormalRom" runat="server" Text="NORMAL ROM" Width="80%" CssClass="lbl"></asp:Label></td>
                <td colspan="2" style="width: 70%; height: 16px">
        <asp:Label ID="lbl_kneepatientRom" runat="server" Text="PATIENT'S ROM / STRENGTH" Width="236px" CssClass="lbl"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 15%;">
                </td>
                <td style="width: 15%;">
                </td>
                <td style="width: 35%; height: 25px">
        <asp:Label ID="lbl_kneepatientRomRight" runat="server" Text="RIGHT" Width="25%" CssClass="lbl"></asp:Label></td>
                <td style="width: 35%; height: 25px">
                    <asp:Label
            ID="lbl_kneepatientRomLeft" runat="server" Text="LEFT" Width="25%" CssClass="lbl"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 15%;">
        <asp:Label ID="lbl_kneeFlexion" runat="server" Text="FLEXION" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%;">
                    <asp:Label
            ID="lbl_135" runat="server" Text="135" CssClass="lbl" Width="20%"></asp:Label></td>
                <td style="width: 35%; height: 25px;">
        <asp:TextBox ID="TXT_KNEE_FLEXION_RIGHT" runat="server" Width="25%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 35%; height: 25px;">
                    <asp:TextBox ID="TXT_KNEE_FLEXION_LEFT" runat="server" Width="25%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 15%">
        <asp:Label ID="lbl_kneeExtension" runat="server" Text="EXTENSION" Width="80%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%">
                    <asp:Label ID="lbl_0_15"
            runat="server" Text="0-15" CssClass="lbl" Width="20%"></asp:Label></td>
                <td style="width: 35%; height: 25px;">
        <asp:TextBox ID="TXT_KNEE_EXTENSION_RIGHT" runat="server" Width="25%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 35%; height: 25px;">
                    <asp:TextBox ID="TXT_KNEE_EXTENSION_LEFT" runat="server" Width="25%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
        </table>
        
                             <table width=100%>
                            <tr>
                                <td style="width: 20%; height: 22px;" class="lbl">
                                    <asp:RadioButtonList ID="RDO_KNEE_CREPITUS_POSITIVE" runat="server" RepeatDirection="Horizontal" Width="100%"><asp:ListItem Value="0">(+)</asp:ListItem>
<asp:ListItem Value="1">(-) CREPITUS</asp:ListItem>
</asp:RadioButtonList>
                                    </td>
                        <td  style="width: 20%; height: 22px;" class="lbl">
                            <asp:RadioButtonList ID="RDO_KNEE_SWELLING_POSITIVE" runat="server" RepeatDirection="Horizontal" Width="100%"><asp:ListItem Value="0">(+)</asp:ListItem>
<asp:ListItem Value="1">(-)SWELLING</asp:ListItem>
</asp:RadioButtonList>
                            </td>
                                <td style="width: 30%; height: 22px;" class="lbl">
                        <asp:RadioButtonList ID="RDO_KNEE_POINT_TENDERNESS_NEGATIVE" runat="server" RepeatDirection="Horizontal" Width="100%"><asp:ListItem Value="0">(+)</asp:ListItem>
<asp:ListItem Value="1">(-) POINT TENDERNESS</asp:ListItem>
</asp:RadioButtonList>
                                    </td>
                                <td style="width: 30%; height: 22px;">
        
        <asp:TextBox ID="TXT_KNEE_POINT_TENDERNESS" runat="server" Width="50%" CssClass="textboxCSS"></asp:TextBox></td>
                            </tr>
                        </table>
                <table width="100%">
                    <tr>
                        <td style="width: 10%" class="lbl">
                        GAIT IS
                        </td>
                        <td style="width: 10%">
                            &nbsp;<asp:checkbox id="CHK_GAIT_INTACT" runat="server" text="INTACT" width="97%"></asp:checkbox></td>
                        <td style="width: 100px" class="lbl">
                            &nbsp;<asp:checkbox id="CHK_GAIT_ANTALGIC" runat="server" text="ANTALGIC"></asp:checkbox></td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                                    </td>
                        <td style="width: 10%" class="lbl">
                            &nbsp;</td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                </table>
                
                           <table width="100%">
            <tr>
                <td style="width: 20%; height: 21px;">
        <asp:Label ID="lbl_Ankle" runat="server" Text="ANKLE" Width="90%" CssClass="lbl" Font-Bold="True" Font-Underline="True"></asp:Label></td>
                <td style="width: 20%; height: 21px;">
                    <asp:Label
            ID="lbl_AnkelnormalRom" runat="server" Text="NORMAL ROM" Width="125px" CssClass="lbl"></asp:Label></td>
                <td style="width: 25%; height: 21px;">
        <asp:Label ID="lbl_AnklepatientRom" runat="server" Text="PATIENT'S ROM / STRENGTH" Width="90%" CssClass="lbl"></asp:Label></td>
                <td style="width: 5%; height: 21px;">
                </td>
                <td style="width: 5%; height: 21px;">
                </td>
                <td style="width: 5%;">
                </td>
                <td style="width: 5%; height: 21px;">
                </td>
                <td style="width: 35%; height: 21px;">
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 21px;">
        <asp:Label ID="lbl_DorsiFlexion" runat="server" Text="DORSI FLEXION" Width="90%" CssClass="lbl"></asp:Label></td>
                <td style="width: 20%; height: 21px;">
        <asp:Label ID="lbl_35" runat="server" Text="35" CssClass="lbl"></asp:Label></td>
                <td style="width: 25%; height: 21px;">
        <asp:TextBox ID="TXT_ANKLE_DORSI_FLEXION_PATIENT_ROM" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 5%; height: 22px;">
        <asp:CheckBox ID="CHK_ANKLE_DORSI_FLEXION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 5%;">
        <asp:CheckBox ID="CHK_ANKLE_DORSI_FLEXION_DULL" runat="server" Text=" " /></td>
                <td style="width: 5%;" class="lbl">
        <asp:CheckBox ID="CHK_ANKLE_DORSI_FLEXION_SHARP" runat="server" Text="S" /></td>
                <td style="width: 5%;" class="lbl">
        <asp:CheckBox ID="CHK_ANKLE_DORSI_FLEXION_SPASM" runat="server" Text="S" /></td>
                <td style="width: 35%; height: 21px;" class="lbl">
        <asp:CheckBox ID="CHK_ANKLE_DORSI_FLEXION_INFLAME" runat="server" Text="I" /></td>
            </tr>
            <tr>
                <td style="width: 20%; height: 21px;" class="lbl">
        <asp:Label ID="lbl_PlantarFlexion" runat="server" Text="PLANTAR FLEXION" Width="90%" CssClass="lbl"></asp:Label></td>
                <td style="width: 20%; height: 21px;" class="lbl">
                    <asp:Label ID="lbl_45" runat="server" Text="45" CssClass="lbl"></asp:Label></td>
                <td style="width: 25%; height: 21px;">
        <asp:TextBox ID="TXT_ANKLE_PLANTAR_FLEXION_PATIENT_ROM" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 5%;">
        <asp:CheckBox ID="CHK_ANKLE_PLANTAR_FLEXION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 5%;" class="lbl">
        <asp:CheckBox ID="CHK_ANKLE_PLANTAR_FLEXION_DULL" runat="server" Text="D" /></td>
                <td style="width: 5%;" class="lbl">
        <asp:CheckBox ID="CHK_ANKLE_PLANTAR_FLEXION_SHARP" runat="server" Text="H" /></td>
                <td style="width: 5%;" class="lbl">
        <asp:CheckBox ID="CHK_ANKLE_PLANTAR_FLEXION_SPASM" runat="server" Text="P" /></td>
                <td style="width: 35%; height: 21px;" class="lbl">
        <asp:CheckBox ID="CHK_ANKLE_PLANTAR_FLEXION_INFLAME" runat="server" Text="N" /></td>
            </tr>
            <tr>
                <td style="width: 20%; height: 21px;">
        <asp:Label ID="lbl_Inversion" runat="server" Text="INVERSION" Width="90%" CssClass="lbl"></asp:Label></td>
                <td style="width: 20%; height: 21px;">
                    <asp:Label ID="lbl_15"
            runat="server" Text="15" CssClass="lbl"></asp:Label></td>
                <td style="width: 25%; height: 21px;">
        <asp:TextBox ID="TXT_ANKLE_INVERSION_PATIENT_ROM" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 5%">
        <asp:CheckBox ID="CHK_ANKLE_INVERSION_NORMAL" runat="server" Text=" " /></td>
                <td style="width: 5%" class="lbl">
        <asp:CheckBox ID="CHK_ANKLE_INVERSION_DULL" runat="server" Text="U" /></td>
                <td style="width: 5%">
        <asp:CheckBox ID="CHK_ANKLE_INVERSION_SHARP" runat="server" Text=" " /></td>
                <td style="width: 5%">
        <asp:CheckBox ID="CHK_ANKLE_INVERSION_SPASM" runat="server" Text=" " /></td>
                <td style="width: 35%; height: 21px;">
        <asp:CheckBox ID="CHK_ANKLE_INVERSION_INFLAME" runat="server" Text="F" /></td>
            </tr>
            <tr>
                <td style="width: 20%; height: 21px;">
        <asp:Label ID="lbl_Eversion" runat="server" Text="EVERSION" Width="90%" CssClass="lbl"></asp:Label></td>
                <td style="width: 20%; height: 21px;">
        <asp:Label ID="lbl_15_1" runat="server" Text="15" CssClass="lbl"></asp:Label></td>
                <td style="width: 25%; height: 21px;">
        <asp:TextBox ID="TXT_ANKLE_EVERSION_PATIENT_ROM" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 5%">
        <asp:CheckBox ID="CHK_ANKLE_EVERSION_NORMAL" runat="server" Width="40%" Text=" " /></td>
                <td style="width: 5%">
        <asp:CheckBox ID="CHK_ANKLE_EVERSION_DULL" runat="server" Width="40%" Text=" " /></td>
                <td style="width: 5%; height: 7px;">
        <asp:CheckBox ID="CHK_ANKLE_EVERSION_SHARP" runat="server" Width="40%" Text=" " /></td>
                <td style="width: 5%">
        <asp:CheckBox ID="CHK_ANKLE_EVERSION_SPASM" runat="server" Width="40%" Text=" " Height="18px" /></td>
                <td style="width: 35%; height: 21px;">
                    <asp:CheckBox ID="CHK_ANKLE_EVERSION_INFLAME" runat="server" Text=" " /></td>
            </tr>
                               <tr>
                                   <td style="width: 20%; height: 21px">
                                   </td>
                                   <td style="width: 20%; height: 21px">
                                   </td>
                                   <td style="width: 25%; height: 21px">
                                   </td>
                                   <td style="width: 5%">
                                   </td>
                                   <td style="width: 5%">
                                   </td>
                                   <td style="width: 5%; height: 7px">
                                   </td>
                                   <td style="width: 5%">
                                   </td>
                                   <td style="width: 35%; height: 21px">
                                   </td>
                               </tr>
        </table>
        
                  <table width="100%">
            <tr>
                <td style="width: 35%; height: 18px;">
                    <asp:Label ID="lbl_ArmAndHand" runat="server" Text="ARM AND HAND" Width="90%" CssClass="lbl" Font-Bold="True" Font-Underline="True"></asp:Label></td>
                <td style="width: 5%; height: 18px;">
        <asp:Label ID="lbl_Right5" runat="server" Text="RIGHT" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 5%; height: 18px;">
        <asp:Label ID="LBL_lEFT5" runat="server" Text="LEFT" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 5%">
                    <asp:Label ID="lbl_both" runat="server" Text="BOTH" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 35%; height: 18px;">
                </td>
                <td style="width: 5%">
        <asp:Label ID="Label1" runat="server" Text="RIGHT" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 5%">
        <asp:Label ID="Label19" runat="server" Text="LEFT" CssClass="lbl" Width="80%"></asp:Label></td>
                <td style="width: 5%">
        <asp:Label ID="Label20" runat="server" Text="BOTH" CssClass="lbl" Width="80%"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 35%;">
        <asp:Label ID="lbl_paininUpperArm" runat="server" Text="PAIN IN UPPER ARM" Width="90%" CssClass="lbl"></asp:Label></td>
                <td colspan="3" style="width: 15%;" class="lbl">
                    &nbsp;<asp:RadioButtonList ID="RDO_PAIN_IN_UPPER_ARM" runat="server" RepeatDirection="Horizontal"
                        Width="82%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
                <td style="width: 35%;">
        <asp:Label ID="lbl_paininWrist" runat="server" Text="PAIN IN WRIST" Width="90%" CssClass="lbl"></asp:Label></td>
                <td colspan="3" style="width: 15%;" class="lbl">
                    <asp:RadioButtonList ID="RDO_PAIN_IN_WRIST" runat="server" RepeatDirection="Horizontal"
                        Width="76%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
            </tr>
            <tr>
                <td style="width: 35%">
        <asp:Label ID="lbl_painInElbow" runat="server" Text="PAIN N ELBOW" Width="90%" CssClass="lbl"></asp:Label></td>
                <td colspan="3" style="width: 15%" class="lbl">
                    <asp:RadioButtonList ID="RDO_PAIN_N_ELBOW" runat="server" RepeatDirection="Horizontal"
                        Width="82%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
                <td style="width: 35%">
        <asp:Label ID="lbl_painInHand" runat="server" Text="PAIN IN HAND" Width="90%" CssClass="lbl"></asp:Label></td>
                <td colspan="3" style="width: 15%" class="lbl">
                    <asp:RadioButtonList ID="RDO_PAIN_IN_HAND" runat="server" RepeatDirection="Horizontal"
                        Width="76%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
            </tr>
            <tr>
                <td style="width: 35%">
        <asp:Label ID="lbl_PainInForeArm" runat="server" Text="PAIN IN FOREARM" Width="90%" CssClass="lbl"></asp:Label></td>
                <td colspan="3" style="width: 15%" class="lbl">
                    <asp:RadioButtonList ID="RDO_PAIN_IN_FOREARM" runat="server" RepeatDirection="Horizontal"
                        Width="82%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
                <td style="width: 35%">
                    <asp:Label ID="lbl_painAndNeedlessinHand"
            runat="server" Text="PAIN & NEEDLESS (HAND)" Width="90%" CssClass="lbl"></asp:Label></td>
                <td colspan="3" style="width: 15%" class="lbl">
                    <asp:RadioButtonList ID="RDO_PAIN_AND_NEEDLESS_HAND" runat="server" RepeatDirection="Horizontal"
                        Width="76%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
            </tr>
            <tr>
                <td style="width: 35%;">
        <asp:Label ID="lbl_painAndNeedless" runat="server" Text="PAIN & NEEDLESS (ARM)" Width="90%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%;" colspan="3" class="lbl">
                    <asp:RadioButtonList ID="RDO_PAIN_AND_NEEDLESS_ARM" runat="server" RepeatDirection="Horizontal"
                        Width="82%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
                <td style="width: 35%;">
        <asp:Label ID="lbl_numbnessinhand" runat="server" Text="NUMBNESS IN HAND" Width="90%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%;" colspan="3" class="lbl">
                    <asp:RadioButtonList ID="RDO_NUMBNESS_IN_HAND" runat="server" RepeatDirection="Horizontal"
                        Width="76%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
            </tr>
            <tr>
                <td style="width: 35%">
        <asp:Label ID="lbl_painAndNeedlessForarm" runat="server" Text="PAIN & NEEDLESS (FOREARM)" Width="90%" CssClass="lbl"></asp:Label></td>
                <td colspan="3" style="width: 15%" class="lbl">
                    <asp:RadioButtonList ID="RDO_PAIN_AND_NEEDLESS_FOREARM" runat="server" RepeatDirection="Horizontal"
                        Width="82%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
                <td style="width: 25%">
                </td>
                <td style="width: 25%" colspan="3">
                </td>
            </tr>
            <tr>
                <td style="width: 35%;">
        <asp:Label ID="lbl_NumbnessInArm" runat="server" Text="NUMBNESS IN ARM" Width="90%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%;" colspan="3" class="lbl">
                    <asp:RadioButtonList ID="RDO_NUMBNESS_IN_ARM" runat="server" RepeatDirection="Horizontal"
                        Width="82%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
                <td style="width: 25%; height: 7px">
                </td>
                <td style="width: 25%;" colspan="3">
                </td>
            </tr>
            <tr>
                <td style="width: 35%;">
        <asp:Label ID="lbl_numbnessinForeArm" runat="server" Text="NUMBNESS IN FOREARM" Width="90%" CssClass="lbl"></asp:Label></td>
                <td style="width: 15%;" colspan="3" class="lbl">
                    <asp:RadioButtonList ID="RDO_NUMBNESS_IN_FOREARM" runat="server" RepeatDirection="Horizontal"
                        Width="83%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
                <td style="width: 25%; height: 15px;">
                </td>
                <td style="width: 25%;" colspan="3">
                </td>
            </tr>
        </table>
        
                <table style="width: 100%; height: 154px">
                    <tr>
                        <td style="width: 15%">
                        </td>
                        <td style="width: 1%">
                        </td>
                        <td style="width: 1%">
                        </td>
                        <td style="width: 10%">
                        </td>
                    </tr>
            <tr>
                <td style="width: 15%">
                    <asp:Label ID="lbl_Feet" runat="server" Text="FEET" Width="90%" CssClass="lbl" Font-Bold="True" Font-Underline="True"></asp:Label></td>
                <td style="width: 1%">
        <asp:Label ID="lbl_FeetRight" runat="server" Text="RIGHT" CssClass="lbl"></asp:Label></td>
                <td style="width: 1%">
        <asp:Label ID="lbl_FeetLeft" runat="server" Text="LEFT" CssClass="lbl"></asp:Label></td>
                <td style="width: 10%">
                    <asp:Label ID="lbl_Feetboth" runat="server" Text="BOTH" CssClass="lbl"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 15%; height: 22px;">
        <asp:Label ID="lbl_FeetAnklepain" runat="server" Text="ANKLE PAIN" Width="90%" CssClass="lbl"></asp:Label></td>
                <td style="width: 85%; height: 22px;" colspan="3" class="lbl">
                    <asp:RadioButtonList ID="RDO_FEET_ANKLE_PAIN" runat="server" RepeatDirection="Horizontal"
                        Width="22%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
            </tr>
            <tr>
                <td style="width: 15%; height: 11px;">
        <asp:Label ID="lbl_feetSwolenAnkle" runat="server" Text="SWOLLEN ANKLE" Width="90%" CssClass="lbl"></asp:Label></td>
                <td colspan="3" style="width: 85%; height: 11px;" class="lbl">
                    <asp:RadioButtonList ID="RDO_FEET_SWOLLEN_ANKLE" runat="server" RepeatDirection="Horizontal"
                        Width="22%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
            </tr>
            <tr>
                <td style="width: 15%">
        <asp:Label ID="lbl_feetFootpain" runat="server" Text="FOOT PAIN" Width="90%" CssClass="lbl"></asp:Label></td>
                <td colspan="3" style="width: 85%" class="lbl">
                    <asp:RadioButtonList ID="RDO_FEET_FOOT_PAIN" runat="server" RepeatDirection="Horizontal"
                        Width="22%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
            </tr>
            <tr>
                <td style="width: 15%">
        <asp:Label ID="lbl_FeetNumbenessofFoot" runat="server" Text="NUMBNESS OF FOOT" Width="90%" CssClass="lbl"></asp:Label></td>
                <td colspan="3" style="width: 85%" class="lbl">
                    <asp:RadioButtonList ID="RDO_FEET_NUMBNESS_OF_FOOT" runat="server" RepeatDirection="Horizontal"
                        Width="22%"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
            </tr>
            <tr>
                <td style="width: 3%; height: 29px;">
        <asp:Label ID="lbl_FeetSwolenFoot" runat="server" Text="SWOLLEN FOOT" Width="90%" CssClass="lbl"></asp:Label></td>
                <td style="width: 85%; height: 29px;" colspan="3" class="lbl">
                    <asp:RadioButtonList ID="RDO_FEET_SWOLLEN_FOOT" runat="server" RepeatDirection="Horizontal"
                        Width="22%" Height="26px"><asp:ListItem Value="0"></asp:ListItem>
<asp:ListItem Value="1"></asp:ListItem>
<asp:ListItem Value="2"></asp:ListItem>
</asp:RadioButtonList>
                    </td>
            </tr>
        </table>
        
          <table width="100%">
                    <tr>
                        <td style="width: 40%; height: 21px">
                            &nbsp; &nbsp; &nbsp;
                        </td>
                        <td style="width: 28%;" class="lbl">
                            &nbsp;<asp:Button ID="BTN_PREVIOUS" runat="server" Text="Previous" Width="39%" OnClick="BTN_PREVIOUS_Click" CssClass="Buttons" />
                            &nbsp; &nbsp; &nbsp; 
                            <asp:Button ID="BTN_SAVE_NEXT" runat="server" Text="Save & Next" Width="42%" OnClick="BTN_SAVE_NEXT_Click" CssClass="Buttons" Height="23px" />
                            &nbsp;&nbsp; &nbsp;
                        </td>
                        <td style="width: 25%; height: 21px">
                            </td>
                        <td style="width: 15%; height: 21px">
                            </td>
                    </tr>
              <tr>
                  <td style="width: 40%; height: 21px">
                            <asp:TextBox ID="TXT_EVENT_ID" runat="server" Width="10%" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TXT_PAIN_AND_NEEDLESS_FOREARM" runat="server" Width="10%" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TXT_PAIN_AND_NEEDLESS_ARM" runat="server" Width="10%" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TXT_PAIN_IN_FOREARM" runat="server" Width="10%" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TXT_PAIN_N_ELBOW" runat="server" Width="10%" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TXT_NUMBNESS_IN_HAND" runat="server" Width="10%" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TXT_PAIN_AND_NEEDLESS_HAND" runat="server" Width="10%" Visible="False"></asp:TextBox></td>
                  <td style="width: 28%; height: 21px">
                      &nbsp; &nbsp; &nbsp;&nbsp;
                  </td>
                  <td style="width: 25%; height: 21px">
                      <asp:TextBox ID="TXT_CERVICAL_SPINE_WITH" runat="server" Width="10%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_CERVICAL_SPINE_MODERATE" runat="server" Width="8%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_CERVICAL_SPINE_TENDERNESS" runat="server" Width="3%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_FORAMINAL_COMPRESSION_SPURLING_SIGN" runat="server" Width="10%" Visible="false"></asp:TextBox><asp:TextBox ID="TXT_FORAMINAL_COMPRESSION_SPURLING_SIGN_POSITIVE" runat="server"
                                        Width="4%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_KNEE_CREPITUS_POSITIVE" runat="server" Width="10%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_KNEE_SWELLING_POSITIVE" runat="server" Width="10%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_KNEE_POINT_TENDERNESS_NEGATIVE" runat="server" Width="10%" Visible="False"></asp:TextBox></td>
                  <td style="width: 25%; height: 21px">
                      <asp:TextBox ID="TXT_STRAIGHT_LEG_RAISING_TEST_SUPINE_RIGHT" runat="server" Height="11px"
                        Width="10%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_STRAIGHT_LEG_RAISING_TEST_SUPINE_LEFT" runat="server" Height="8px"
                        Width="10%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_PAIN_IN_UPPER_ARM" runat="server" Width="10%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_PAIN_IN_WRIST" runat="server" Width="10%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_PAIN_IN_HAND" runat="server" Width="10%" Visible="False"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 40%; height: 21px">
                    <asp:TextBox ID="TXT_FEET_SWOLLEN_FOOT" runat="server" Width="8%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_FEET_FOOT_PAIN" runat="server" Width="8%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_FEET_ANKLE_PAIN" runat="server" Width="8%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_NUMBNESS_IN_FOREARM" runat="server" Width="10%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_NUMBNESS_IN_ARM" runat="server" Width="10%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_CERVICAL_SPINE_APPEAR" runat="server" Width="10%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_FEET_SWOLLEN_ANKLE" runat="server" Width="8%" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_FEET_NUMBNESS_OF_FOOT" runat="server" Width="8%" Visible="False"></asp:TextBox></td>
                  <td style="width: 28%; height: 21px">
                      &nbsp; &nbsp; &nbsp;&nbsp;
                  </td>
                  <td style="width: 25%; height: 21px">
                  </td>
                  <td style="width: 25%; height: 21px">
                  </td>
              </tr>
                </table>      
        
                
 

 </td> 
 </tr>
 </table>
 </asp:Content>
