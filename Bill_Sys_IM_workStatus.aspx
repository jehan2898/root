<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_IM_workStatus.aspx.cs" Inherits="Bill_Sys_IM_workStatus" Title="Untitled Page" %>

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
                        <td style="width: 31%">
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
            <td width="100%" class="TDPart">
                <table width="100%">
                    <tr>
                        <td class="lbl" width="50%">
                            <b>WORK STATUS</b></td>
                        <td class="lbl" width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="50%">
                            HAS THE PATIENT MISSED WORK BECAUSE OF THE INJURY / ILNESS?
                        </td>
                        <td class="lbl" width="50%">
                            <asp:RadioButtonList ID="rdlPatient_Missed_Work" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value= "0" >Yes</asp:ListItem>
                                <asp:ListItem Value= "1" >Yes No IF YES, DATE PATIENT FIRST MISSED  </asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td class="lbl" width="50%">
                            WORK :
                        </td>
                        <td class="lbl" width="50%">
                            <asp:TextBox ID="txtPatient_Missed_Work_Date" Text="" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="50%">
                            IS THE PATIENT CURRENTLY WORKING?</td>
                        <td class="lbl" width="50%">
                            <asp:RadioButtonList ID="rdlPatient_Currently_Working" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value= "0" >Yes</asp:ListItem>
                                <asp:ListItem Value= "1" >No IF YES, DID THE PATIENT RETURN TO:  </asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td class="lbl" width="50%">
                            <asp:RadioButtonList ID="rdlPatient_Return_To_Work" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value= "0" >USUAL WORK ACTIVITIES</asp:ListItem>
                                <asp:ListItem Value= "1" >- LIMITED WORK ACTIVITIES  </asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td class="lbl" width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="50%">
                            CAN PATIENT RETURN TO WORK?
                        </td>
                        <td class="lbl" width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="50%" style="height: 20px">
                            <asp:CheckBox ID="chkPatient_CanNot_Work_Ret" runat="server" Text=" THE PATIENT CANNOT RETURN TO WORK BECAUSE (EXPLAIN) " /></td>
                        <td class="lbl" width="50%" style="height: 20px">
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="2">
                            <asp:TextBox ID="txtPatient_CanNot_Work_Ret" runat="server" Text="" Width="96%"></asp:TextBox></td>
                    </tr>
                </table>
                
                
                 <table width="100%" visible="false">
                <tr>
                <td>
                <asp:TextBox  ID="txtrdlPatient_Return_To_Work" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td>
                <asp:TextBox  ID="txtrdlPatient_Currently_Working" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td>
                <asp:TextBox  ID="txtrdlPatient_Missed_Work" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="70%" class="lbl">
                            <asp:CheckBox ID="chkPatient_Can_Work_Without_Limit" runat="server" Text="- THE PATIENT CAN RETURN TO WORK WITHOUT LIMITATIONS ON ">
                            </asp:CheckBox></td>
                        <td width="30%" class="lbl">
                            <asp:TextBox ID="txtPatient_Can_Work_Without_Limit" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="lbl" width="70%">
                            <asp:CheckBox ID="chkPatient_Can_Work_Limit" runat="server" Text="- THE PATIENT CAN RETURN TO WORK WITH THE FOLLOWING LMIITATIONS ON  "
                                Width="100%" /></td>
                        <td class="lbl" width="30%" style="height: 22px">
                            <asp:TextBox ID="txtPatient_Can_Work_With_Limit" runat="server" Text=""></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" style="height: 20px">
                            <asp:CheckBox ID="chkPatient_Work_Limit_Bending_Twisting" runat="server" Text="-BENDING/TWISTING" /></td>
                        <td class="lbl" style="height: 20px">
                            <asp:CheckBox ID="chkPatient_Work_Limit_Lifting" runat="server" Text="- LIFTING" /></td>
                        <td class="lbl" style="height: 20px">
                            <asp:CheckBox ID="chkPatient_Work_Limit_Sitting" runat="server" Text="- SITTING" /></td>
                    </tr>
                    <tr>
                        <td class="lbl">
                            <asp:CheckBox ID="chkPatient_Work_Limit_Climbing_Stairs_Ladders" runat="server" Text="- CLIMBING STAIRS/LADDERS" /></td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkPatient_Work_Limit_Operating_Heavy_EQuip" runat="server" Text="- OPERATING HEAVY EQUIPMENT" /></td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkPatient_Work_Limit_Standing" runat="server" Text="- STANDING" /></td>
                    </tr>
                    <tr>
                        <td class="lbl">
                            <asp:CheckBox ID="chkPatient_Work_Limit_Environmental_Condi" runat="server" Text="- ENVIRONMENTAL CONDITIONS" /></td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkPatient_Work_Limit_Operation_Motor_Vhcle" runat="server" Text="- OPERATION OF MOTOR VEHICLES" /></td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkPatient_Work_Limit_Use_Public_Traspt" runat="server" Text="- USE OF PUBLIC TRANSPORTATION" /></td>
                    </tr>
                    <tr>
                        <td class="lbl">
                            <asp:CheckBox ID="chkPatient_Work_Limit_Kneeling" runat="server" Text="- KNEELING" /></td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkPatient_Work_Limit_Personal_Protive_Equip" runat="server" Text="- PERSONAL PROTECTIVE EQUIPMENT" /></td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkPatient_Work_Limit_Use_Upper_Extremities" runat="server" Text="- USE OF UPPER EXTREMITIES" /></td>
                    </tr>
                    <tr>
                        <td class="lbl" style="height: 22px">
                            <asp:CheckBox ID="chkPatient_Work_Limit_Other" runat="server" Text="- OTHER " /></td>
                        <td class="lbl" colspan="2" style="height: 22px">
                            <asp:TextBox ID="txtPatient_Work_Limit_Other" runat="server" Text="" Width="95%"></asp:TextBox></td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            DESCRIBE / QUANTIFY THE LIMITATIONS:
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:TextBox ID="txtDescribeLimitation" runat="server" TextMode="MultiLine" Width="100%"
                                Height="40%"> </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%" class="lbl">
                    <tr>
                        <td>
                            HOW LONG WILL THESE LIMITATIONS APPLY?
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkLimitApply1To2Days" runat="server" Text="- 1-2 DAYS" />
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkLimitApply3To7Days" runat="server" Text="- 3-7 DAYS" />
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkLimitApply8To14Days" runat="server" Text="- 8-14 DAY" />
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkLimitApply15PlusDays" runat="server" Text="- 15+ DAYS" />
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkLimitApplyUnknown" runat="server" Text="- UNKNOWN" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td width="100%" class="lbl">
                            WITH WHOM WILL YOU DISCUSS THE PATIENT’S RETURNING TO WORK AND /OR LIMITATION?
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:RadioButtonList ID="rdlDiscussLimitPatient" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value= "0" >- WITH PATIENT</asp:ListItem>
                                <asp:ListItem Value= "1" >- WITH PATIENT’S EMPLOYER</asp:ListItem>
                                <asp:ListItem Value="2">- N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:CheckBox ID="chkProvideService" runat="server" Text="-I PROVIDED THE SERVICES LISTED ABOVE." />
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:CheckBox ID="chkSuperviseService" runat="server" Text=" - I ACTIVELY SUPERVISED THE HEALTH-CARE PROVIDER NAMED BELOW WHO PROVIDED THESE SERVICES." />
                        </td>
                    </tr>
                </table>
                 <table width="100%" visible="false">
                <tr>
                <td>
                <asp:TextBox  ID="txtrdlDiscussLimitPatient" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center">
                         <asp:TextBox ID="txtEventID" runat="server" Visible="false" width="2px"></asp:TextBox>
                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click" />
                            <asp:Button ID="btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
