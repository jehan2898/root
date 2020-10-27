<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_IM_PhysicalStatus1.aspx.cs" Inherits="Bill_Sys_IM_PhysicalStatus1" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                        <td style="width: 32%">
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
                        <td style="width: 32%">
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
            <td width="100%" class="TDPart" style="height: 218px">
                <%--<table width="100%">
                    <tr>
                        <td width="25%" class="lbl">
                            Patient's Name
                        </td>
                        <td width="40%">
                            <asp:TextBox ID="TextBox1" runat="server" Width="90%"></asp:TextBox>
                        </td>
                        <td width="10%" class="lbl">
                            Date of accident
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="DOA3" runat="server" Width="60%"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender11" runat="server" TargetControlID="DOA3"
                                PopupButtonID="imgbtnFromDate" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" class="lbl" width="20%">
                            Date of Examination</td>
                        <td colspan="1" width="20%">
                            <asp:TextBox ID="DOE4" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender12" runat="server" TargetControlID="DOE4"
                                PopupButtonID="imgbtnFromDate" />
                        </td>
                    </tr>
                </table>--%>
                <table width="100%">
                    <tr>
                        <td class="lbl">
                        <strong>PHYSICAL STATUS PART-2</strong></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="lbl">
                            FORWARD MOVMENT -
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkCervicalSpineForwordMovement" runat="server" />
                        </td>
                        <td class="lbl">
                            BACKWORD MOVMENT -
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkCervicalSpineBackwordMovement" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl">
                            ROTATE HEAD LEFT -
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkCervicalSpineRotateHeadLeft" runat="server" />
                        </td>
                        <td class="lbl">
                            ROTATE HEAD RIGHT -
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkCervicalSpineRotateHeadRight" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl">
                            BEND NECK LEFT -
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkCervicalSpineBendNeckLeft" runat="server" />
                        </td>
                        <td class="lbl">
                            BEND NECK RIGHT -
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkCervicalSpineBendNeckRight" runat="server" />
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
                <table>
                    <tr>
                        <td class="lbl">
                            CERVICAL MUSCLES APPEAR :
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:RadioButtonList ID="rdlCervicalSpineAppear" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" Width="870px">
                                <asp:ListItem Value="0">SYMMETRICAL / </asp:ListItem>
                                <asp:ListItem Value="1">ASSYMETRICAL</asp:ListItem>
                                <asp:ListItem Value="2"> WITH / </asp:ListItem>
                                <asp:ListItem Value="3"> WITHOUT </asp:ListItem>
                                <asp:ListItem Value="4"> MODERATE /  </asp:ListItem>
                                <asp:ListItem Value="5"> SEVERE </asp:ListItem>
                                <asp:ListItem Value="6"> MUSCLE SPASM TO UPPER TRAPEZIUS AND PARASPINAL MUSCLES. </asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                
                  <table width="100%" visible="false">
                <tr>
                <td>
                <asp:TextBox  ID="txtrdlCervicalSpineAppear" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td style="height: 18px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="80%">
                    <tr>
                        <td class="lbl" width="10%">
                            LUMBOSACRAL SPINE
                        </td>
                        <td class="lbl" width="10%">
                            NORMAL ROM
                        </td>
                        <td class="lbl" width="25%">
                            PATIENT’S ROM / STRENGTH
                        </td>
                    
                        
                    </tr>
                    <tr>
                        <td class="lbl" width="17%">
                            FLEXION
                        </td>
                        <td class="lbl" style="width: 10%">
                            90
                        </td>
                        <td class="lbl" width=" 10%">
                            <asp:TextBox ID="txtLumbosacralSpineFlexionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineFlexionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineFlexionDull" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineFlexionSharp" Text="s" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineFlexioSpasm" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineFlexioSpasmInflame" Text="I" runat="server" />
                     
                    </tr>
                    <tr>
                        <td class="lbl" width="17%" style="height: 5px">
                            EXTENSION
                        </td>
                        <td class="lbl" width ="10%" style="height: 5px">
                            30
                        </td>
                        <td class="lbl" width="10%" style="height: 5px">
                            <asp:TextBox ID="txtLumbosacralSpineExtensionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%" style="height: 5px">
                            <asp:CheckBox ID="chkLumbosacralSpineExtensionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%" style="height: 5px">
                            <asp:CheckBox ID="chkLumbosacralSpineExtensionDull" Text="D" runat="server" />
                        </td>
                        <td class="lbl" width="5%" style="height: 5px">
                            <asp:CheckBox ID="chkLumbosacralSpineExtensionSharp" Text="H" runat="server" />
                        </td>
                        <td class="lbl" width="5%" style="height: 5px">
                            <asp:CheckBox ID="chkLumbosacralSpineExtensionSpasm" Text="P" runat="server" />
                        </td>
                        <td class="lbl" width="5%" style="height: 5px">
                            <asp:CheckBox ID="chkLumbosacralSpineExtensionInflame" Text="N" runat="server" />
                       
                    </tr>
                    <tr>
                        <td class="lbl" width="17%">
                            LEFT ROTATION
                        </td>
                        <td class="lbl" style="width: 10%">
                            30
                        </td>
                        <td class="lbl" width="10%">
                            <asp:TextBox ID="txtLumbosacralSpineLeftRotaionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineLeftRotaionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineLeftRotaionDull" Text="U" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineLeftRotaionSharp" Text="A" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="ChecchkLumbosacralSpineLeftRotaionSpasm" Text="A" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineLeftRotaionInflame" Text="F" runat="server" />
                        </td>
                     
                    </tr>
                    <tr>
                        <td class="lbl" width="17%" >
                            RIGHT ROTATION
                        </td>
                        <td class="lbl" width="10%;">
                            30
                        </td>
                        <td class="lbl" width="10%" >
                            <asp:TextBox ID="txtLumbosacralSpinerightRoatationPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpinerightRoatationNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpinerightRoatationDull" Text="L" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpinerightRoatationSharp" Text="R" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpinerightRoatationSpasm" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpinerightRoatationInflame" Text="L" runat="server" />
                        </td>
              
                    </tr>
                    <tr>
                        <td class="lbl" width="17%">
                            LT LATERAL FLEXION
                        </td>
                        <td class="lbl" style="width: 10%">
                            20
                        </td>
                        <td class="lbl" width="10%">
                            <asp:TextBox ID="txtLumbosacralSpineLTLateralFlexionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineLTLateralFlexionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineLTLateralFlexionDull" Text="L" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineLTLateralFlexionSharp" Text="P" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineLTLateralFlexionSpasm" Text="M" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineLTLateralFlexionInflame" Text="A" runat="server" />
                        </td>
                     
                    </tr>
                    <tr>
                        <td class="lbl" width="17%">
                            RT LATERAL FLEXION
                        </td>
                        <td class="lbl" style="width: 10%">
                            20
                        </td>
                        <td class="lbl" width="10%">
                            <asp:TextBox ID="txtLumbosacralSpineRTLateralFlexionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineRTLateralFlexionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineRTLateralFlexionDull" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineRTLateralFlexionSharp" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineRTLateralFlexionSpasm" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLumbosacralSpineRTLateralFlexionInflame" Text="M" runat="server" />
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
                        <td class="lbl" width="15%">
                            UPPER LUMBAR PAIN
                        </td>
                        <td class="lbl" width="50%">
                            <asp:RadioButtonList ID="rdlUpperLumbarPain" runat="server" Width="24%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Left</asp:ListItem>
                                <asp:ListItem Value="1">Right</asp:ListItem>
                                <asp:ListItem Value="2">Both</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                        &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="15%">
                            LOWER LUMBAR PAIN
                        </td>
                        <td class="lbl" width="50%">
                            <asp:RadioButtonList ID="rdlLowerLumbarPain" runat="server" Width="24%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Left</asp:ListItem>
                                <asp:ListItem Value="1">Right</asp:ListItem>
                                <asp:ListItem Value="2">Both</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="15%">
                            SACRO-ILIAC PAIN
                        </td>
                        <td class="lbl" width="50%">
                            <asp:RadioButtonList ID="rdlSacroIllacPain" runat="server" Width="24%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Left</asp:ListItem>
                                <asp:ListItem Value="1">Right</asp:ListItem>
                                <asp:ListItem Value="2">Both</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="15%">
                            MUSCLE SPASM
                        </td>
                        <td class="lbl" width="50%">
                            <asp:RadioButtonList ID="rdlMuscleSpasm" runat="server" Width="24%" RepeatDirection="Horizontal" >
                                <asp:ListItem Value="0">Left</asp:ListItem>
                                <asp:ListItem Value="1">Right</asp:ListItem>
                                <asp:ListItem Value="2">Both</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                
                <table width="100%" visible="false">
                <tr>
                <td>
                <asp:TextBox  ID="txtrdlMuscleSpasm" runat="server" Visible="false" Width="2px"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlSacroIllacPain" runat="server" Visible="false" Width="2px"></asp:TextBox>
                </td>
                
                  <td>
                <asp:TextBox  ID="txtrdlLowerLumbarPain" runat="server" Visible="false" Width="2px"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlUpperLumbarPain" runat="server" Visible="false" Width="2px"></asp:TextBox>
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
                        <td class="lbl" style="width: 29%; height: 47px">
                            STRAIGHT LEG RAISING TEST SUPINE
                        </td>
                        <td class="lbl" style="height: 47px">
                            <asp:RadioButtonList ID="rdlLegRaisingRight" runat="server" Width="90%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+)</asp:ListItem>
                                <asp:ListItem Value="1">(-)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl">
                            RIGHT
                        </td>
                        <td class="lbl" >
                            <asp:RadioButtonList ID="rdlLegRaisingLeft" runat="server" Width="90%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+)</asp:ListItem>
                                <asp:ListItem Value="1">(-)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl" style="height: 47px">
                            LEFT
                        </td>
                    </tr>
                </table>
                <table width="100%" visible="false">
                <tr>
                <td>
                <asp:TextBox  ID="txtrdlLegRaisingRight" runat="server" Visible="false" width="2px"></asp:TextBox>
                </td>
                <td>
                <asp:TextBox  ID="txtrdlLegRaisingLeft" runat="server" Visible="false" width="2px"></asp:TextBox>
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
                        <td class="lbl" width="15%" >
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
                        <td class="lbl" width="15%">
                            FLEXION
                        </td>
                        <td class="lbl" width="10%">
                            100
                        </td>
                        <td class="lbl" width="20%">
                            <asp:TextBox ID="txtHIPFlexionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPFlexionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPFlexionDull" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPFlexionSharp" Text="s" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPFlexionSpasm" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPFlexionInflame" Text="I" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="15%" >
                            EXTENSION
                        </td>
                        <td class="lbl" width="10%" style="height: 27px">
                            100
                        </td>
                        <td class="lbl" width="20%" style="height: 27px">
                            <asp:TextBox ID="txtHIPExtensionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%" style="height: 27px">
                            <asp:CheckBox ID="chkHIPExtensionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%" style="height: 27px">
                            <asp:CheckBox ID="chkHIPExtensionDull" Text="D" runat="server" />
                        </td>
                        <td class="lbl" width="5%" style="height: 27px">
                            <asp:CheckBox ID="chkHIPExtensionSharp" Text="H" runat="server" />
                        </td>
                        <td class="lbl" width="5%" style="height: 27px">
                            <asp:CheckBox ID="chkHIPExtensionSpasm" Text="P" runat="server" />
                        </td>
                        <td class="lbl" width="5%" style="height: 27px">
                            <asp:CheckBox ID="chkHIPExtensionInflame" Text="N" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="15%">
                            ABDUCTION
                        </td>
                        <td class="lbl" width="10%">
                            40
                        </td>
                        <td class="lbl" width="20%">
                            <asp:TextBox ID="txtHIPAbductionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPAbductionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPAbductionDull" Text="U" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPAbductionSharp" Text="A" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPAbductionSpasm" Text="A" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPAbductionInflame" Text="F" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="15%">
                            ADDUCTION
                        </td>
                        <td class="lbl" width="10%" >
                            30
                        </td>
                        <td class="lbl" width="20%" >
                            <asp:TextBox ID="txtHIPAdductionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%" >
                            <asp:CheckBox ID="txtHIPaAdductionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%" >
                            <asp:CheckBox ID="txtHIPaAdductionDull" Text="L" runat="server" />
                        </td>
                        <td class="lbl" width="5%" >
                            <asp:CheckBox ID="txtHIPaAdductionSharp" Text="R" runat="server" />
                        </td>
                        <td class="lbl" width="5%" >
                            <asp:CheckBox ID="txtHIPaAdductionSpasm" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="5%" >
                            <asp:CheckBox ID="txtHIPaAdductionInflame" Text="L" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="15%">
                            INTERNAL ROTATION
                        </td>
                        <td class="lbl" width="10%">
                            40
                        </td>
                        <td class="lbl" width="20%">
                            <asp:TextBox ID="txtHIPInternalRotationPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPInternalRotationNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPInternalRotationDull" Text="L" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPInternalRotationSharp" Text="P" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPInternalRotationSpasm" Text="M" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPInternalRotationInflame" Text="A" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="15%">
                            EXTERNAL ROTATION
                        </td>
                        <td class="lbl" width="10%">
                            40
                        </td>
                        <td class="lbl" width="20%">
                            <asp:TextBox ID="txtHIPExternalRotationPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPExternalRotationNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPExternalRotationDull" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPExternalRotationSharp" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPExternalRotationSpasm" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHIPExternalRotationInflame" Text="M" runat="server" />
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
                        <td class="lbl">
                            TENDERNESS OVER
                        </td>
                        <td class="lbl">
                            <asp:RadioButtonList ID="rdlHipTenderness" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">ANTERIOR / </asp:ListItem>
                                <asp:ListItem Value="1">LATERAL /</asp:ListItem>
                                <asp:ListItem Value="2">POSTERIOR ASPECTS.</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    
                </table>
                <table width="100%" visible="false">
                <tr>
                <td>
                <asp:TextBox  ID="txtrdlHipTenderness" runat="server" Visible="false"></asp:TextBox>
                </td>
                
                </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center"width="50%">
                            <asp:TextBox ID="txtEventID" runat="server" Visible="false"></asp:TextBox>
                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click"  />
                            <asp:Button ID="btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="btnSave_Click" />
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

