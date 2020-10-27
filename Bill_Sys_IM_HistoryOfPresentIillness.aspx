<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_IM_HistoryOfPresentIillness.aspx.cs" Inherits="Bill_Sys_IM_HistoryOfPresentIillness"
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
                        <td class="lbl" style="width: 13%">
                            Patient's Name
                        </td>
                        <td style="width: 23%">
                            <asp:TextBox ID="txtPatientName" runat="server" Width="83%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="lbl" style="width: 10%">
                            Date of accident
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="txtDOA" runat="server" Width="70%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 13%; height: 20px" >
                            Date of Examination</td>
                        <td style="width: 23%; height: 20px;">
                            <asp:TextBox ID="txtDOE" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                           
                        </td>
                        <td class="lbl" style="width: 13%; height: 20px;">
                            Date of birth</td>
                        <td width="25%" style="height: 20px">
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
                <table width="100%">
                    <tr>
                        <td colspan="6" align="left" class="lbl">
                            <b>HISTORY OF PRESENT ILLNESS</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="left" class="lbl">
                            Based on the patient’s history, where and how did the injury/illness happen :
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="left" width="100%">
                            <asp:TextBox ID="txtWhereHowInjuryHappen" runat="server" TextMode="MultiLine" Width="70%" Height="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="lbl" width="40%">
                            On the date of injury/illness what was the patient's job title and/or description
                            :
                        </td>
                        <td width="30%" align="right" class="lbl">
                            <asp:TextBox ID="txtPatientJobTitle" runat="server" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="lbl" width="40%">
                            On the date of injury/illness what was the patient's usual work activities :
                        </td>
                        <td width="30%" align="left" class="lbl">
                            <asp:TextBox ID="txtPatientWorkActivity" runat="server" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="25%">
                            <asp:RadioButtonList ID="rdlInjuryIllness" runat="server" Width="90%" RepeatDirection="Horizontal" >
                                <asp:ListItem Value="0">Patient</asp:ListItem>
                                <asp:ListItem Value="1">MedicalRecords</asp:ListItem>
                                <asp:ListItem Value="2">Other Specify</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="30%" class="lbl">
                            <asp:TextBox ID="txtOtherSpecify" runat="server" Width="95%"></asp:TextBox></td>
                            <td>
                            &nbsp;
                            </td>
                    </tr>
                </table>
                <table>
                <tr>
                <td>
                <asp:TextBox ID="txtrdlInjuryIllness" runat="server" Visible="false"></asp:TextBox>
                </td>
                
                </tr>
                </table>
                <table>
                    <tr>
                        <td colspan="2" width="65%" class="lbl">
                            Did another health provider treat this injury/illness including hospitalization
                            and/or surgery ?
                        </td>
                        <td colspan="1" width="15%" class="lbl">
                            <asp:RadioButtonList ID="rdlTreatInjury" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">YES</asp:ListItem>
                                <asp:ListItem Value="1">NO</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td colspan="2" width="25%" class="lbl">
                            If yes, give details</td>
                    </tr>
                </table>
                <table>
                <tr>
                <td>
                <asp:TextBox ID="txtrdlTreatInjury" runat="server" Visible="false"></asp:TextBox>
                </td>
                
                </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:TextBox ID="txtAnotherHealthProviderTreatedDatails" runat="server" TextMode="MultiLine"
                                Height="60px" Width="70%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" style="height: 18px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="30%" class="lbl">
                            Location of other medical provider :
                        </td>
                        <td>
                            <asp:TextBox ID="txtLocationOtherProvider" runat="server" Width="60%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="100%" align="left" class="lbl">
                            How the patient arrived to the office today:
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" style="height: 20px">
                            <asp:CheckBox ID="chkBus" Text="Bus" runat="server" /></td>
                        <td class="lbl" style="height: 20px">
                            <asp:CheckBox ID="chkSubWay" Text="Subway" runat="server" /></td>
                        <td class="lbl" style="height: 20px">
                            <asp:CheckBox ID="chkTaxi" Text="Taxi" runat="server" /></td>
                        <td class="lbl" style="height: 20px">
                            <asp:CheckBox ID="chkWalking" Text="Walking" runat="server" /></td>
                        <td class="lbl" style="height: 20px">
                            <asp:CheckBox ID="chkAmbulate" Text="Ambulate" runat="server" /></td>
                        <td class="lbl" style="height: 20px">
                            <asp:CheckBox ID="chkPrivateCar" Text="Private Car" runat="server" /></td>
                    </tr>
                    <tr>
                        <td colspan="1" width="10%" class="lbl">
                            <asp:CheckBox ID="chkOTHER" Text="Other" runat="server" />
                        </td>
                        <td colspan="5" width="80%" class="lbl">
                            <asp:TextBox ID="txtPatientOtherTransport" runat="server" Width="60%"></asp:TextBox></td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td colspan="2" width="60%" class="lbl">
                            Have you previously treated this patient for a similar work-related injury/illness?
                        </td>
                        <td colspan="1" width="15%" class="lbl">
                            <asp:RadioButtonList ID="rdlRDOPreviouslyTreated" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">YES</asp:ListItem>
                                <asp:ListItem Value="1">NO</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td colspan="2" width="25%" class="lbl">
                            No If yes,when</td>
                    </tr>
                    <tr>
                        <td colspan="4" width="100%" class="lbl">
                            <asp:TextBox ID="txtPreviousTreatmentWorkRelatedInjury" runat="server" TextMode="MultiLine"
                                Height="60px" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" width="100%" class="lbl">
                            <b>TREATMENT RENDERED TODAY : </b>
                        </td>
                    </tr>
                </table>
                
                
                       <table>
                <tr>
                <td>
                <asp:TextBox ID="txtrdlRDOPreviouslyTreated" runat="server" Visible="false"></asp:TextBox>
                </td>
                
                </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="50%" class="lbl">
                            <asp:CheckBox ID="chk101" Text="99241 – Initial visit"
                                runat="server" />
                        </td>
                        <td width="50%" class="lbl">
                            <asp:CheckBox ID="chk104" Text="99244 – Initial visit"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="lbl">
                            <asp:CheckBox ID="chk102" Text="99242 – Initial visit"
                                runat="server" />
                        </td>
                        <td width="50%" class="lbl">
                            <asp:CheckBox ID="chk105" Text="99245 – Initial visit"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="lbl">
                            <asp:CheckBox ID="chk103" Text="99243 – Initial visit"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width= "100%" colspan= "3">
                <asp:TextBox ID="txtEventID" runat="server" Visible="false" Width="2px"></asp:TextBox>
                            <asp:Button ID="btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
