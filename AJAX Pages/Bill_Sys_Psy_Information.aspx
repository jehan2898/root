<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_Psy_Information.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Psy_Information" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx"TagName="MessageControl"
    TagPrefix="UserMessage" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td colspan="3" scope="col">
                                              <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
                                                <div align="left" class="blocktitle">
                                                    <div id="divPlanOfCare" class="div_blockcontent">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="3">
                                                            <tr>
                                                                <td colspan="4" style="height: 30px">
                                                                    <div id="ErrorDiv" visible="true" style="color: Red;">
                                                                        &nbsp;</div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px; height: 19px">
                                                                </td>
                                                                <td colspan="3" style="height: 19px; text-align: left">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 227px">
                                                                                ATTENDING PSYCHOLOGIST'S REPORT:</td>
                                                                            <td style="width: 200px">
                                                                                <asp:RadioButtonList ID="rbattenting_psy" runat="server" RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="1">45 HR INITIAL</asp:ListItem>
                                                                                    <asp:ListItem Value="2">15 DAY INITIAL</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="3">90 DAY</asp:ListItem>
                                                                                </asp:RadioButtonList></td>
                                                                            <td style="width: 327px">
                                                                                SERVICES PROVIDED UNDER WCB PREFERRED PROVIDER ORGANIZATION (PPO) PROGRAM?</td>
                                                                            <td style="width: 100px">
                                                                                <asp:RadioButtonList ID="rbserviceprovider" runat="server" RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                                                    <asp:ListItem Value="3" Selected="True">None</asp:ListItem>
                                                                                </asp:RadioButtonList></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td colspan="1" style="height: 19px; text-align: left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px; height: 19px">
                                                                </td>
                                                                <td colspan="3" style="height: 19px; text-align: left">
                                                                    HISTORY:</td>
                                                                <td colspan="1" style="height: 19px; text-align: left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px; height: 19px;">
                                                                    1.</td>
                                                                <td colspan="3" style="height: 19px; text-align: left">
                                                                    Describe incident or occupational history that precipitated onset of related symptoms:</td>
                                                                <td colspan="1" style="height: 19px; text-align: left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px; height: 19px">
                                                                </td>
                                                                <td colspan="5" style="height: 19px; text-align: left">
                                                                    <asp:TextBox ID="txtincident" runat="server" TextMode="MultiLine" Width="56%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px; height: 19px">
                                                                    2.</td>
                                                                <td colspan="3" style="height: 19px; text-align: left">
                                                                    Has patient given any history of pre-existing psychological impairment? If so, describe
                                                                    specifically.</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px; height: 19px">
                                                                </td>
                                                                <td colspan="5" style="height: 19px; text-align: left">
                                                                    <asp:TextBox ID="txthistory" runat="server" TextMode="MultiLine" Width="56%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td colspan="2" style="text-align: left">
                                                                    EVALUATION/TREATMENT:</td>
                                                                <td colspan="2" style="text-align: left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                    3.&nbsp;</td>
                                                                <td style="text-align: left" colspan="2">
                                                                    Referral was for</td>
                                                                <td colspan="2" style="text-align: left">
                                                                    <asp:RadioButtonList ID="rdbreferral" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">Evaluation Only (Complete item a)</asp:ListItem>
                                                                        <asp:ListItem Value="2">Treatment Only (Complete item b-1,2)</asp:ListItem>
                                                                        <asp:ListItem Value="3" Selected="True">Evaluation and Treatment (Complete items a and b-1,2)</asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                    a.</td>
                                                                <td colspan="2" style="text-align: left">
                                                                    <%--<div class="lbl">--%>
                                                                    Your Evalution :<%--</div>--%></td>
                                                                <td style="height: 28px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td colspan="3" style="height: 28px">
                                                                    <asp:TextBox ID="txtEvaluation" runat="server" TextMode="MultiLine" Width="90%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                    b.(1)
                                                                </td>
                                                                <td colspan="3" style="text-align: left">
                                                                    Patient's condition and progress:
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td colspan="3" style="text-align: left">
                                                                    <asp:TextBox ID="txtcondition" runat="server" TextMode="MultiLine" Width="90%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px" valign="top">
                                                                    b.(2)</td>
                                                                <td colspan="5" style="text-align: left;">
                                                                    Treatment and planned future treatment. If an authorization request is required
                                                                    (see items 4 & 5 on reverse),
                                                                    <asp:CheckBox ID="chkTreatment" runat="server" />
                                                                    and explain below. If additional space is necessary, please attach request</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px" valign="top">
                                                                </td>
                                                                <td colspan="5" style="text-align: left">
                                                                    <asp:TextBox ID="txtTreatment" runat="server" TextMode="multiline" Width="90%"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                            <tr>
                                                                <td style="width: 28px">
                                                                    4.</td>
                                                                <td class="tablecellControl" style="height: 24px; width: 266px;">
                                                                    <div class="lbl">
                                                                        Dates of visits on which this report is based</div>
                                                                </td>
                                                                <td style="width: 190px">
                                                                    <asp:TextBox ID="txtVisitDate" runat="server"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgbtnPatientDOB" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtVisitDate"
                                                                        PopupButtonID="imgbtnPatientDOB" PopupPosition="BottomRight" />
                                                                </td>
                                                                <td class="tablecellControl" style="height: 24px; width: 192px;">
                                                                    <div class="lbl">
                                                                        Date of First Visit</div>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtFirstVisitDate" runat="server"></asp:TextBox>
                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFirstVisitDate"
                                                                        PopupButtonID="ImageButton1" PopupPosition="BottomRight" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td style="text-align: left">
                                                                    Will patient be seen again?</td>
                                                                <td colspan="3">
                                                                    <asp:RadioButtonList ID="rdnpatientseen" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                                        <asp:ListItem Value="3" Selected="True">None</asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Panel ID="Panel1" runat="server">
                                                                        <table class="lbl" width="100%" border="0">
                                                                            <tr>
                                                                                <td class="tablecellLabel" style="height: 24px;">
                                                                                    <div class="lbl">
                                                                                        If yes, when:</div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtPatientSeen" runat="server"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="tablecellControl" style="height: 24px">
                                                                                    <div class="lbl">
                                                                                        If no, was patient referred back to attending doctor:</div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="rbpatientAttendingDoctor" runat="server" RepeatDirection="Horizontal">
                                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                                                        <asp:ListItem Value="3" Selected="True">None</asp:ListItem>
                                                                                    </asp:RadioButtonList></td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                    5.</td>
                                                                <td colspan="1" style="text-align: left">
                                                                    Is Patient working ?</td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rbPatientWorking" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                                        <asp:ListItem Value="3" Selected="True">None</asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td colspan="4">
                                                                    <asp:Panel ID="Panel2" runat="server">
                                                                        <table class="lbl" width="88%">
                                                                            <tr>
                                                                                <td class="tablecellLabel" style="height: 24px;" colspan="3">
                                                                                    <div class="lbl">
                                                                                        If yes, date(s) patient: resumed limited work of any kind</div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtlimitedwork" runat="server"></asp:TextBox>
                                                                                </td>
                                                                                <td class="tablecellControl" style="height: 24px" colspan="3">
                                                                                    <div class="lbl">
                                                                                        resumed regular work</div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtregularwork" runat="server"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td colspan="4">
                                                                    CR:</td>
                                                                <td colspan="1">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                    6.</td>
                                                                <td colspan="3">
                                                                    Was the occurrence described above (or in your previous report) the competent producing
                                                                    cause of the injury or disability (if any) sustained?</td>
                                                                <td colspan="1">
                                                                <asp:RadioButtonList ID="rbsustained" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                                        <asp:ListItem Value="3" Selected="True">None</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td colspan="4">
                                                                    REMRKS:</td>
                                                                <td colspan="1">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                    7.</td>
                                                                <td colspan="4">
                                                                    Enter here additional pertinent information
                                                                </td>
                                                                <td colspan="1">
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td colspan="4">
                                                                    <asp:TextBox ID="txtadditionalinfo" runat="server" TextMode="MultiLine" Width="67%"></asp:TextBox></td>
                                                                <td colspan="1">
                                                                </td>
                                                            </tr>
                                                                   <tr>
                                                                <td>
                                                                    </td>
                                                                <td>
                                                                    Patient's Account Number :
                                                                </td>
                                                                <td>
                                                                <asp:TextBox ID="txtPatientAccNo" runat="server" Width="200px"></asp:TextBox>
                                                                </td>
                                                            </tr>                                                         
                                                            <tr>
                                                                <td style="width: 28px; height: 97px">
                                                                </td>
                                                                <td colspan="2" style="height: 97px">
                                                                    *If treatment was under the VFBL or VAWBL show as "Employer" the liable political
                                                                    subdivision and check one:</td>
                                                                <td colspan="1" style="height: 97px">
                                                                    <asp:RadioButtonList ID="rbvfblorvawbl" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">VFBL</asp:ListItem>
                                                                        <asp:ListItem Value="2">VAWBL</asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td>
                                                                    If you have filed a previous report, setting forth a history of the injury, enter
                                                                    its date</td>
                                                                <td colspan="1">
                                                                    <asp:TextBox ID="txtdateof_forth_history" runat="server"></asp:TextBox><asp:ImageButton ID="imgbtnforthhistory" runat="server" ImageUrl="~/Images/cal.gif" /><ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtdateof_forth_history"
                                                                        PopupButtonID="imgbtnforthhistory" PopupPosition="BottomRight" />
                                                                </td>
                                                                <td colspan="1">
                                                                    and complete Items 3 to 18. If not, complete ALL items.</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td colspan="1">
                                                                    &nbsp;</td>
                                                                <td colspan="1">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td colspan="1">
                                                                    <asp:Button ID="btnSaveAndGoToNext" runat="server" Text="Save" OnClick="btnSaveAndGoToNext_Click" Width="80px" CssClass="btn-gray" />
                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn-gray" /></td>
                                                                <td colspan="1">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div id="div_contect_field_buttons">
                                                        <table>
                                                            <tr>
                                                                <td colspan="4" align="center" style="width: 100%">
                                                                    &nbsp;
                                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                    &nbsp; &nbsp;
                                                                    <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>

</asp:Content>

