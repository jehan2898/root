<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Bill_Sys_CO_Patient_Intake4.aspx.cs" Inherits="Bill_Sys_CO_Patient_Intake4" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table class="ContentTable" width="100%">
        <tr>
            <td class="TDPart" align="center" valign="top" style="height: 226px">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblFormHeader" runat="server" Text="PATIENT  INTAKE - (PAGE  IV of IV)" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="height: 21px">
                        </td>
                    </tr>
                </table>
                <table width="99%">
                    <tbody>
                        <tr>
                            <td align="left" colspan="6" valign="middle">
                                <asp:Label ID="lbl_FamilyHistory" runat="server" Font-Bold="True" Text="FAMILY HISTORY"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_Mother" runat="server" Text="Mother"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 6px">
                                </td>
                            <td align="left" colspan="4" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_MOTHER_DIABETES" runat="server" Text="Diabetes" />
                                <asp:CheckBox ID="CHK_MOTHER_HEART_ATTACK" runat="server" Text="Heart Attack" />
                                <asp:CheckBox ID="CHK_MOTHER_KIDNEY" runat="server" Text="Kidney" />
                                <asp:CheckBox ID="CHK_MOTHER_CANCER" runat="server" Text="Cancer" />
                                <asp:CheckBox ID="CHK_MOTHER_BACK" runat="server" Text="Back" />
                                <asp:CheckBox ID="CHK_MOTHER_OTHER_CONDITION" runat="server" Text="Other Condition" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_MotherOtherCondition" runat="server" Text="Other Condition" Width="102px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 6px">
                                </td>
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_MOTHER_OTHER_CONDITION" runat="server" Height="50px" TextMode="MultiLine" Width="487px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_Father" runat="server" Text="Father"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 6px">
                                </td>
                            <td align="left" colspan="4" valign="middle" class="lbl">
                            <asp:CheckBox ID="CHK_FATHER_DIABETES" runat="server" Text="Diabetes" />
                            <asp:CheckBox ID="CHK_FATHER_HEART_ATTACK" runat="server" Text="Heart Attack" />
                            <asp:CheckBox ID="CHK_FATHER_KIDNEY" runat="server" Text="Kidney" />
                            <asp:CheckBox ID="CHK_FATHER_CANCER" runat="server" Text="Cancer" />
                            <asp:CheckBox ID="CHK_FATHER_BACK" runat="server" Text="Back" />
                            <asp:CheckBox ID="CHK_FATHER_OTHER_CONDITION" runat="server" Text="Other Condition" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_FatherOtherCondition" runat="server" Text="Other Condition"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 6px">
                                </td>
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_FATHER_OTHER_CONDITION" runat="server" Height="50px" TextMode="MultiLine" Width="487px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_Brothers" runat="server" Text="Brother(s)"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 6px">
                                </td>
                            <td align="left" colspan="4" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_BROTHER_DIABETES" runat="server" Text="Diabetes" />
                                <asp:CheckBox ID="CHK_BROTHER_HEART_ATTACK" runat="server" Text="Heart Attack" />
                                <asp:CheckBox ID="CHK_BROTHER_KIDNEY" runat="server" Text="Kidney" />
                                <asp:CheckBox ID="CHK_BROTHER_CANCER" runat="server" Text="Cancer" />
                                <asp:CheckBox ID="CHK_BROTHER_BACK" runat="server" Text="Back" />
                                <asp:CheckBox ID="CHK_BROTHER_OTHER_CONDITION" runat="server" Text="Other Condition" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_BrotherOtherCondition" runat="server" Text="Other Condition"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 6px">
                                </td>
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_BROTHER_OTHER_CONDITION" runat="server" Height="50px" TextMode="MultiLine" Width="487px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_Sisters" runat="server" Text="Sister(s)"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 6px">
                                </td>
                            <td align="left" colspan="4" valign="middle" class="lbl">
                               <asp:CheckBox ID="CHK_SISTER_DIABETES" runat="server" Text="Diabetes" />
                                <asp:CheckBox ID="CHK_SISTER_HEART_ATTACK" runat="server" Text="Heart Attack" />
                                <asp:CheckBox ID="CHK_SISTER_KIDNEY" runat="server" Text="Kidney" />
                                <asp:CheckBox ID="CHK_SISTER_CANCER" runat="server" Text="Cancer" />
                                <asp:CheckBox ID="CHK_SISTER_BACK" runat="server" Text="Back" />
                                <asp:CheckBox ID="CHK_SISTER_OTHER_CONDITION" runat="server" Text="Other Condition" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_SisterOtherCondition" runat="server" Text="Other Condition"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 6px">
                                </td>
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_SISTER_OTHER_CONDITION" runat="server" Height="50px" TextMode="MultiLine" Width="487px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_PatientSignatureDate" runat="server" Text="Patient Signature Date" Visible="False"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 6px">
                                </td>
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_DATE" runat="server" Width="110px" Visible="False"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="TDPart" valign="top" style="height: 52px">
                <table style="width: 100%">
                    <tr>
                        <td align="center">
                        <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click" />
                <asp:Button ID="css_btnSave" runat="server" Text="Save" CssClass="Buttons" OnClick="css_btnSave_Click" />
                <asp:Button ID="btnSavePrint" runat="server" CssClass="Buttons" Text="Save & Print" OnClick="btnSavePrint_Click"/>
                </td>
                
                    </tr>
                </table>
                <asp:TextBox ID="TXT_I_EVENT" runat="server" Visible="false">1</asp:TextBox></td>
        </tr>
    </table>

</asp:content>

