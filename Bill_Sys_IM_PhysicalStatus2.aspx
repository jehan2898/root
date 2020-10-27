<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_IM_PhysicalStatus2.aspx.cs" Inherits="Bill_Sys_IM_PhysicalStatus2"
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
                        <td class="lbl" style="width: 14%">
                            Date of accident
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="txtDOA" runat="server" Width="70%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="20%" >
                            Date of Examination</td>
                        <td style="width: 31%">
                            <asp:TextBox ID="txtDOE" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="lbl" style="width: 14%">
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
                <%--<table width="100%">
                    <tr>
                        <td class="lbl" style="width: 24%">
                            Patient's Name
                        </td>
                        <td width="40%">
                            <asp:TextBox ID="txtPatientName6" runat="server" Width="90%"></asp:TextBox>
                        </td>
                        <td width="20%" class="lbl">
                            Date of accident
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtDAO6" runat="server" Width="60%"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton20" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDAO6"
                                PopupButtonID="imgbtnFromDate" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" class="lbl" style="width: 24%">
                            Date of Examination</td>
                        <td colspan="1" width="20%">
                            <asp:TextBox ID="txtDoE6" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton21" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txtDoE6"
                                PopupButtonID="imgbtnFromDate" />
                        </td>
                    </tr>
                </table>--%>
                <table width="100%">
                      <tr>
                        <td class="lbl" width="10%" style="height: 15px">
                            KNEE&nbsp;</td>
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
                            135</td>
                        <td class="lbl" width="20%">
                            <asp:TextBox ID="txtKneeFlexionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkKneeFlexionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkKneeFlexionDull" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkKneeFlexionSharp" Text="s" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkKneeFlexionSpasm" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkKneeFlexionInflame" Text="I" runat="server" />
                        </td>
                    </tr>
                          <tr>
                        <td class="lbl" width="10%" style="height: 27px">
                            EXTENSION
                        </td>
                        <td class="lbl" width="10%" style="height: 27px">
                            0-15
                        </td>
                        <td class="lbl" width="20%" style="height: 27px">
                            <asp:TextBox ID="txtKneeExtensionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%" style="height: 27px">
                            <asp:CheckBox ID="chkKneeExtensionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%" style="height: 27px">
                            <asp:CheckBox ID="chkKneeExtensionDull" Text="D" runat="server" />
                        </td>
                        <td class="lbl" width="5%" style="height: 27px">
                            <asp:CheckBox ID="chkKneeExtensionSharp" Text="H" runat="server" />
                        </td>
                        <td class="lbl" width="5%" style="height: 27px">
                            <asp:CheckBox ID="chkKneeExtensionSpasm" Text="P" runat="server" />
                        </td>
                        <td class="lbl" width="5%" style="height: 27px">
                            <asp:CheckBox ID="chkKneeExtensionInflame" Text="N" runat="server" />
                        </td>
                    </tr>
                
                </table>
                <table width="100%">
                    
                    <tr>
                        <td class="lbl" width="20%">
                            Point Tenderness
                        </td>
                        <td class="lbl" width="5%">
                        </td>
                        <td class="lbl" colspan="2">
                            <asp:RadioButtonList ID="rdlKnee_Point_Tenderness" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Postive </asp:ListItem>
                                <asp:ListItem Value="1">Negative</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="20%">
                            Crepitus
                        </td>
                        <td class="lbl" width="5%">
                        </td>
                        <td class="lbl">
                            <asp:RadioButtonList ID="rdlKnee_Crepitis" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Postive </asp:ListItem>
                                <asp:ListItem Value="1">Negative</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="20%" style="height: 39px">
                            Effusion
                        </td>
                        <td class="lbl" width="5%" style="height: 39px">
                        </td>
                        <td class="lbl" style="height: 39px">
                            <asp:RadioButtonList ID="rdl_Knee_Effusion" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Postive </asp:ListItem>
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
                            <asp:RadioButtonList ID="rdlknee_Joint_Line_Pain" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Postive </asp:ListItem>
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
                            <asp:RadioButtonList ID="rdlknee_Swlling" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Postive </asp:ListItem>
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
                  <table width="100%" visible="false">
                <tr>
                <td>
                <asp:TextBox  ID="txtrdlknee_Swlling" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td>
                <asp:TextBox  ID="txtrdlknee_Joint_Line_Pain" runat="server" Visible="false"></asp:TextBox>
                </td>
                
                <td>
                <asp:TextBox  ID="txtrdl_Knee_Effusion" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td>
                <asp:TextBox  ID="txtrdlKnee_Crepitis" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                
                <tr>
                <td>
                <asp:TextBox  ID="txtrdlKnee_Point_Tenderness" runat="server" Visible="false"></asp:TextBox>
                </td>
                
                </tr>
                </table>
                
                <table width="100%">
                    <tr>
                        <td class="lbl" width="20%">
                            Vlgus Test
                        </td>
                        <td class="lbl" width="5%">
                        </td>
                        <td class="lbl" width="25%">
                            <asp:RadioButtonList ID="rdlValgus_Test_Right" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+) </asp:ListItem>
                                <asp:ListItem Value="1">(-)-Right</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl">
                            <asp:RadioButtonList ID="rdlValgus_Test_Left" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+) </asp:ListItem>
                                <asp:ListItem Value="1">(-)-Left</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="20%">
                            Varus Test
                        </td>
                        <td class="lbl" width="5%">
                        </td>
                        <td class="lbl" width="25%">
                            <asp:RadioButtonList ID="rdlVarus_Test_Right" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+) </asp:ListItem>
                                <asp:ListItem Value="1">(-)-Right</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl" style="height: 15px">
                            <asp:RadioButtonList ID="rdlVarus_Test_Left" runat="server" RepeatDirection="Horizontal">
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
                            <asp:RadioButtonList ID="rdlMcmurrays_Test_Right" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+) </asp:ListItem>
                                <asp:ListItem Value="1">(-)-Right</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl">
                            <asp:RadioButtonList ID="rdlMcmurrays_Test_Left" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+) </asp:ListItem>
                                <asp:ListItem Value="1">(-)-Left</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="20%" style="height: 39px">
                            Anterior Drawer Sign
                        </td>
                        <td class="lbl" width="5%" style="height: 39px">
                        </td>
                        <td class="lbl" width="25%" style="height: 39px">
                            <asp:RadioButtonList ID="rdlAnterior_Drawer_Sign_Right" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+) </asp:ListItem>
                                <asp:ListItem Value="1">(-)-Right</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl" style="height: 39px">
                            <asp:RadioButtonList ID="rdlAnterior_Drawer_Sign_Left" runat="server" RepeatDirection="Horizontal">
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
                            <asp:RadioButtonList ID="rdlPosterior_Drawer_Sign_Right" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+) </asp:ListItem>
                                <asp:ListItem Value="1">(-)-Right</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl">
                            <asp:RadioButtonList ID="rdlPosterior_Drawer_Sign_Left" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+) </asp:ListItem>
                                <asp:ListItem Value="1">(-)-Left</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                  <table width="100%" visible="false">
                <tr>
                <td>
                <asp:TextBox  ID="txtrdlPosterior_Drawer_Sign_Left" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlPosterior_Drawer_Sign_Right" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlAnterior_Drawer_Sign_Left" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlAnterior_Drawer_Sign_Right" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                
                  <tr>
                <td>
                <asp:TextBox  ID="txtrdlMcmurrays_Test_Left" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlMcmurrays_Test_Right" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlVarus_Test_Left" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlVarus_Test_Right" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                  <tr>
                <td>
                <asp:TextBox  ID="txtrdlValgus_Test_Left" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlValgus_Test_Right" runat="server" Visible="false"></asp:TextBox>
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
                        <td class="lbl" width="20%">
                        </td>
                        <td class="lbl">
                        </td>
                        <td class="lbl">
                            Right &nbsp; &nbsp; Left &nbsp; &nbsp; &nbsp; Both</td>
                        <td style="height: 18px">
                        </td>
                        <td class="lbl" style="height: 18px">
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
                            <asp:RadioButtonList ID="rdlPain_In_Upper_Arm" runat="server" RepeatDirection="Horizontal"
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
                            <asp:RadioButtonList ID="rdlPain_In_Wrist" runat="server" RepeatDirection="Horizontal"
                                Width="60%">
                                <asp:ListItem Value="1"></asp:ListItem>
                                <asp:ListItem Value="0"></asp:ListItem>
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
                            <asp:RadioButtonList ID="rdlPain_N_Elbow" runat="server" Width="60%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1"></asp:ListItem>
                                <asp:ListItem Value="0"></asp:ListItem>
                                <asp:ListItem Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl">
                            Pain In Hand
                        </td>
                        <td class="lbl">
                            <asp:RadioButtonList ID="rdlPain_In_Hand" runat="server" Width="60%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1"></asp:ListItem>
                                <asp:ListItem Value="0"></asp:ListItem>
                                <asp:ListItem  Value="2"></asp:ListItem>
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
                            <asp:RadioButtonList ID="rdlPain_In_Forearm" runat="server" Width="60%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1"></asp:ListItem>
                                <asp:ListItem Value="0"></asp:ListItem>
                                <asp:ListItem Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl">
                            Pain & Needless (Hand)
                        </td>
                        <td class="lbl">
                            <asp:RadioButtonList ID="rdlPain_Needless_Hand" runat="server" Width="60%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1"></asp:ListItem>
                                <asp:ListItem Value="0"></asp:ListItem>
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
                            <asp:RadioButtonList ID="rdlPain_Needless_Arm" runat="server" Width="60%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="2"></asp:ListItem>
                                <asp:ListItem Value="0"></asp:ListItem>
                                <asp:ListItem Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl">
                            Numbness In Hand</td>
                        <td class="lbl">
                            <asp:RadioButtonList ID="rdlNumbness_In_Hand" runat="server" Width="60%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1"></asp:ListItem>
                                <asp:ListItem Value="0"></asp:ListItem>
                                <asp:ListItem Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="20%">
                            Pain &amp; Needless(Forearm)</td>
                        <td width="5%">
                        </td>
                        <td class="lbl" width="25%">
                            <asp:RadioButtonList ID="rdlPain_Needless_Forearm" runat="server" Width="60%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1"></asp:ListItem>
                                <asp:ListItem Value="0"></asp:ListItem>
                                <asp:ListItem Value="2"></asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td class="lbl">
                        </td>
                        <td class="lbl">
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="20%">
                            Numbness In Arm</td>
                        <td width="5%">
                        </td>
                        <td class="lbl" width="25%">
                            <asp:RadioButtonList ID="rdlNumbness_In_Arm" runat="server" Width="60%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1"></asp:ListItem>
                                <asp:ListItem Value="0"></asp:ListItem>
                                <asp:ListItem Value="2"></asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td class="lbl">
                        </td>
                        <td class="lbl">
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="20%">
                            Numbness In Forearm</td>
                        <td width="5%">
                        </td>
                        <td class="lbl" width="25%">
                            <asp:RadioButtonList ID="rdlNumbness_In_Forearm" runat="server" Width="60%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1"></asp:ListItem>
                                <asp:ListItem Value="0"></asp:ListItem>
                                <asp:ListItem Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl">
                        </td>
                        <td class="lbl">
                        </td>
                    </tr>
                </table>
                
                
                  <table width="100%" visible="false">
                <tr>
                <td>
                <asp:TextBox  ID="txtrdlNumbness_In_Forearm" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlNumbness_In_Arm" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlPain_Needless_Forearm" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlNumbness_In_Hand" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                
                  <tr>
                <td>
                <asp:TextBox  ID="txtrdlPain_Needless_Arm" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlPain_Needless_Hand" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlPain_In_Forearm" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlPain_In_Hand" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                  <tr>
                <td>
                <asp:TextBox  ID="txtrdlPain_N_Elbow" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td>
                <asp:TextBox  ID="txtrdlPain_In_Wrist" runat="server" Visible="false"></asp:TextBox>
                </td>
                  <td>
                <asp:TextBox  ID="txtrdlPain_In_Upper_Arm" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                </table>
                <table width="70%">
                   <%-- <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>--%>
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
                   <%-- <tr>
                        <td width="">
                            <b>ANKLE</b>
                        </td>
                        <td class="lbl">
                            Normal Rom
                        </td>
                        <td class="lbl">
                            Patient's Rom /Strength
                        
                    </tr>--%>
                    <tr>
                        <td class="lbl" width="10%" style="height: 15px">
                            Dorst Flexion
                        </td>
                        <td class="lbl" width="10%" style="height: 15px">
                            35</td>
                        <td class="lbl" width="20%" style="height: 15px">
                            <asp:TextBox ID="txtAnkle_Dorst_Flexion_Patient_Rom" Text="" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Dorst_Flexion_Normal" runat="server" Text="" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Dorst_Flexion_Dull" runat="server" Text=""></asp:CheckBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Dorst_Flexion_Sharp" runat="server" Text=""></asp:CheckBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Dorst_Flexion_Spasm" runat="server" Text="S"></asp:CheckBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Dorst_Flexion_Inflame" runat="server" Text="I"></asp:CheckBox>
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
                            <asp:TextBox ID="txtAnkle_Planter_Flexion_Patient_Rom" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Planter_Flexion_Normal" runat="server" Text=""></asp:CheckBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Planter_Flexion_Dull" runat="server" Text="D"></asp:CheckBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Planter_Flexion_Sharp" runat="server" Text="H"></asp:CheckBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Planter_Flexion_Spasm" runat="server" Text="P"></asp:CheckBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Planter_Flexion_Inflame" runat="server" Text="N"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="10%" style="height: 15px">
                            INVERSION</td>
                        <td class="lbl" width="10%" style="height: 15px">
                            15</td>
                        <td class="lbl" width="20%" style="height: 15px">
                            <asp:TextBox ID="txtAnkle_Inversion_Patient_Rom" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Inversion_Normal" runat="server" Text=""></asp:CheckBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Inversion_Dull" runat="server" Text="U"></asp:CheckBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Inversion_Sharp" runat="server" Text="U"></asp:CheckBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Inversion_Spasm" runat="server" Text="A"></asp:CheckBox></td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Inversion_Inflame" runat="server" Text="F"></asp:CheckBox></td>
                        
                    </tr>
                    <tr>
                        <td class="lbl" width="10%" style="height: 15px">
                            EVERSION</td>
                        <td class="lbl" width="10%" style="height: 15px">
                            15</td>
                        <td class="lbl" width="20%" style="height: 15px">
                            <asp:TextBox ID="txtAnkle_Eversion_Patient_Rom" Text="" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Eversion_Normal" runat="server" Text=""></asp:CheckBox></td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Eversion_Dull" runat="server" Text="L"></asp:CheckBox></td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Eversion_Sharp" runat="server" Text="R"></asp:CheckBox></td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Eversion_Spasm" runat="server" Text="S"></asp:CheckBox></td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAnkle_Eversion_Inflame" runat="server" Text="L"></asp:CheckBox></td>
                        
                    </tr>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td colspan="5" >
                            </td>
                        </tr>
                        <tr>
                            <td class="lbl" width="20%" colspan="5">
                                <b>FEET</b></td>
                        </tr>
                        <tr>
                            <td class="lbl" width="20%">
                            </td>
                            <td class="lbl">
                            </td>
                            <td class="lbl">
                                Right &nbsp; &nbsp; Left &nbsp; &nbsp; &nbsp; Both</td>
                            <td>
                            </td>
                            <td class="lbl">
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
                                    <asp:ListItem Value="0"> </asp:ListItem>
                                    <asp:ListItem Value="2"> </asp:ListItem>
                                    <asp:ListItem Value="3"> </asp:ListItem>
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
                                    <asp:ListItem Value="0"></asp:ListItem>
                                    <asp:ListItem Value="1"></asp:ListItem>
                                    <asp:ListItem Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="lbl">
                                &nbsp;</td>
                            <td class="lbl">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="lbl" width="20%">
                                FOOT PAIN
                            </td>
                            <td width="5%">
                            </td>
                            <td class="lbl" width="25%">
                                <asp:RadioButtonList ID="rdlFeet_Foot_Pain" runat="server" Width="60%" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0"></asp:ListItem>
                                    <asp:ListItem Value="1"></asp:ListItem>
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
                                NUMBNESS OF FOOT
                            </td>
                            <td width="5%">
                            </td>
                            <td class="lbl" width="25%">
                                <asp:RadioButtonList ID="rdlFeet_Numbness_Of_Foot" runat="server" Width="60%" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0"></asp:ListItem>
                                    <asp:ListItem Value="1"></asp:ListItem>
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
                                SWOLLEN FOOT</td>
                            <td width="5%">
                            </td>
                            <td class="lbl" width="25%">
                                <asp:RadioButtonList ID="rdlFeet_Swollen_Foot" runat="server" Width="60%" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0"></asp:ListItem>
                                    <asp:ListItem Value="1"></asp:ListItem>
                                    <asp:ListItem Value="2"></asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td class="lbl">
                            </td>
                            <td class="lbl">
                            </td>
                        </tr>
                    </tbody>
                </table>
                  <table width="100%" visible="false">
                <tr>
                <td>
                <asp:TextBox  ID="txtrdlFeet_Swollen_Foot" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td>
                <asp:TextBox  ID="txtrdlFeet_Numbness_Of_Foot" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td>
                <asp:TextBox  ID="txtrdlFeet_Foot_Pain" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td>
                <asp:TextBox  ID="txtrdlFeet_Swollen_Ankle" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td>
                <asp:TextBox  ID="txtrdlFeet_Ankle_Pain" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                </table>
                <%--<table width="100%">
                    <tr>
                        <td class="lbl" style="width: 24%">
                            Patient's Name
                        </td>
                        <td width="40%">
                            <asp:TextBox ID="txtPatientName7" runat="server" Width="90%"></asp:TextBox>
                        </td>
                        <td width="20%" class="lbl">
                            Date of accident
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtDAO1" runat="server" Width="60%"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton22" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="txtDAO1"
                                PopupButtonID="imgbtnFromDate" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" class="lbl" style="width: 24%">
                            Date of Examination</td>
                        <td colspan="1" width="20%">
                            <asp:TextBox ID="txtDoE7" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton23" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender14" runat="server" TargetControlID="txtDoE7"
                                PopupButtonID="imgbtnFromDate" />
                        </td>
                    </tr>
                </table>--%>
                <table width="100%">
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="height: 20px">
                            <b>GAIT:</b></td>
                        <td class="lbl" style="height: 20px">
                            <asp:CheckBox ID="chkGait_Normal" runat="server" Text="Normal/"></asp:CheckBox>
                        </td>
                        <td class="lbl" style="height: 20px">
                            <asp:CheckBox ID="chkGait_Slow" runat="server" Text="Slow gait/"></asp:CheckBox>
                        </td>
                        <td class="lbl" style="height: 20px">
                            <asp:CheckBox ID="chkGaint_Antalgic" runat="server" Text="Antalgic /"></asp:CheckBox>
                        </td>
                        <td class="lbl" width="20%" style="height: 20px">
                            <asp:CheckBox ID="chkGaint_Foot_Drop" runat="server" Text="Foot droop (steppage) /">
                            </asp:CheckBox>
                        </td>
                        <td class="lbl" style="height: 20px">
                            <asp:CheckBox ID="chkGaint_hemiplegic" runat="server" Text="Hemiplegic."></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="100%" colspan="6">
                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click"  />
                            <asp:Button ID="btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="btnSave_Click" />
                            <asp:TextBox ID="txtEventID" runat="server" Visible="false" Width="50px"></asp:TextBox>
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
