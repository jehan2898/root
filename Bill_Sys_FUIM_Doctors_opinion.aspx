<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_FUIM_Doctors_opinion.aspx.cs" Inherits="Bill_Sys_FUIM_Doctors_opinion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td width="100%" class="TDPart">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="width: 100%; height: 48px;">
                            <table width="100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblError" runat="server" Text=" " Visible="False" Width="50%"></asp:Label></td>
                                    <td style="width: 15%">
                                    </td>
                                    <td style="width: 10%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Label ID="lbl_patientsname" runat="server" Text=" Patient's Name " CssClass="lbl"
                                            Width="60%"></asp:Label></td>
                                    <td style="width: 45%">
                                        <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="80%" CssClass="textboxCSS"
                                            BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black"
                                            ReadOnly="true"></asp:TextBox></td>
                                    <td style="width: 15%">
                                        <asp:Label ID="lbl_dateofAccident" runat="server" Text=" Date of Accident " CssClass="lbl"
                                            Width="70%"></asp:Label></td>
                                    <td style="width: 10%">
                                        <asp:TextBox ID="TXT_DOA" runat="server" CssClass="textboxCSS" Width="90%" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        <asp:Label ID="lbl_DOS" runat="server" Text="DOS" CssClass="lbl" Width="50%"></asp:Label></td>
                                    <td style="width: 45%;">
                                        <asp:TextBox ID="TXT_DOS" runat="server" Width="25%" BackColor="Transparent" BorderColor="Transparent"
                                            BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                    <td style="width: 15%;">
                                    </td>
                                    <td style="width: 10%;">
                                    </td>
                                </tr>
                            </table>
                            <asp:TextBox ID="TXT_EVENT_ID" runat="server" Height="13px" Width="5%" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="ContentLabel" style="text-align: left;">
                            <table width="100%" style="text-align: left;">
                                <tr>
                                    <td style="width: 96%; height: 21px;" class="lbl">
                                        <asp:Label ID="lbl_DoctorsDescription" runat="server" Width="25%" CssClass="lbl"
                                            Text="DOCTOR’S OPINION" Font-Underline="True" align="left"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td class="lbl" style="width: 73%">
                                                    <asp:Label ID="lbl_CompitentmedicalCouse" runat="server" Text="WAS THE INCIDENT THAT THE PATIENT DESCRIBEDTHE COMPETENT MEDICAL CAUSE OF THIS INJURY/ILNESS?"
                                                        CssClass="lbl" Width="90%"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:RadioButtonList ID="RDO_CAUSE_OF_INJURY_YES" runat="server" Height="21px" RepeatDirection="Horizontal"
                                                        Width="15%">
                                                        <asp:ListItem Value="0">Yes</asp:ListItem>
                                                        <asp:ListItem Value="1">No</asp:ListItem>
                                                    </asp:RadioButtonList></td>
                                            </tr>
                                            <tr>
                                                <td class="lbl" style="width: 73%">
                                        <asp:Label ID="lbl_PatientsComplaintCosistentWithinjury" runat="server" Text="ARE THE PATIENT’S COMPLAINTS CONSISTENT WITH HIS/HER HISTORY OF THIS INJURY/ILNESS?"
                                            CssClass="lbl" Width="99%"></asp:Label></td>
                                                <td style="text-align: left">
                                        <asp:RadioButtonList ID="RDO_COMPLAINT_CONSISTENT_YES" runat="server" RepeatDirection="Horizontal"
                                            Width="25%" CssClass="lbl">
                                            <asp:ListItem Value="0">Yes</asp:ListItem>
                                            <asp:ListItem Value="1">No</asp:ListItem>
                                        </asp:RadioButtonList></td>
                                            </tr>
                                            <tr>
                                                <td class="lbl" style="width: 73%">
                                        <asp:Label ID="lbl_patientsijutyCosistentWithobjective" runat="server" Text="IS THE PATIENT’S HISTORY OF THE INJURY/ILNESS CONSISTENT WITH OBJECTIVE FINDINGS?"
                                            CssClass="lbl" Width="98%"></asp:Label></td>
                                                <td style="text-align: left">
                                        <asp:RadioButtonList ID="RDO_CONSISTENT_OBJ_FIDINGS_YES" runat="server" RepeatDirection="Horizontal"
                                            Width="99%" CssClass="lbl">
                                            <asp:ListItem Value="0">Yes</asp:ListItem>
                                            <asp:ListItem Value="1">No</asp:ListItem>
                                            <asp:ListItem Value="2">N/A(NO FINDINGS AT THIS TIME)</asp:ListItem>
                                        </asp:RadioButtonList></td>
                                            </tr>
                                            <tr>
                                                <td class="lbl" style="width: 73%">
                                        <asp:Label ID="lbl_percentageofTemporaryimpairment" runat="server" Text=" WHAT IS THE PERCENTAGE (0-100%) OF TEMPORARY IMPAIRMENT?"
                                            CssClass="lbl" Width="100%"></asp:Label></td>
                                                <td style="text-align: left">
                                        <asp:TextBox ID="TXT_PERCENTAGE_TEMP_IMPAIRMENT" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox>%</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 
                    <tr>
                        <td class="ContentLabel" style="text-align: left;">
                            <table width="100%">
                                <tr>
                                    <td style="width: 36%" class="lbl">
                                    </td>
                                    <td style="width: 12%" class="lbl">
                                        <asp:Button ID="BTN_PREVIOUS" runat="server" Text="Previous" Width="80%" OnClick="BTN_PREVIOUS_Click"
                                            CssClass="Buttons" /></td>
                                    <td style="width: 25%" class="lbl">
                                        <asp:Button ID="BTN_SAVE_NEXT" runat="server" Text="Save & Next" Width="39%" OnClick="BTN_SAVE_NEXT_Click"
                                            CssClass="Buttons" /></td>
                                    <td style="width: 20%" class="lbl">
                                    </td>
                                </tr>
                            </table>
                            <asp:TextBox ID="TXT_COMPLAINT_CONSISTENT_YES" runat="server" Height="12px" Width="2px"
                                Visible="False"></asp:TextBox>
                            <asp:TextBox ID="TXT_CAUSE_OF_INJURY_YES" runat="server" Height="12px" Width="2"
                                Visible="False"></asp:TextBox>
                           
                            <asp:TextBox ID="TXT_CONSISTENT_OBJ_FIDINGS_YES" runat="server" Width="5%" Visible="False"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
