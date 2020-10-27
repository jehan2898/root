<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CM_workStatus.aspx.cs" Inherits="Bill_Sys_CM_workStatus" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table class="ContentTable" width="100%">
        <tr>
            <td class="TDPart">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblHeading" runat="server" Text="INITIAL EXAMINATION" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td>
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
                                        <td class="lbl" width="50%">
                                            <b>WORK STATUS</b></td>
                                        <td class="lbl" width="50%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%" style="height: 39px">
                                            HAS THE PATIENT MISSED WORK BECAUSE OF THE INJURY / ILNESS?
                                        </td>
                                        <td class="lbl" width="50%" style="height: 39px">
                                            <asp:RadioButtonList ID="RDO_PATIENT_MISSED_WORK" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                                <asp:ListItem Value="1">No IF YES, DATE PATIENT FIRST MISSED  </asp:ListItem>
                                            </asp:RadioButtonList></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            WORK :
                                        </td>
                                        <td class="lbl" width="50%">
                                            <asp:TextBox ID="TXT_PATIENT_MISSED_WORK_DATE" Text="" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            IS THE PATIENT CURRENTLY WORKING?</td>
                                        <td class="lbl" width="50%">
                                            <asp:RadioButtonList ID="RDO_PATIENT_CURRENTLY_WORKING" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                                <asp:ListItem Value="1">No IF YES, DID THE PATIENT RETURN TO:  </asp:ListItem>
                                            </asp:RadioButtonList></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="50%">
                                            <asp:RadioButtonList ID="RDO_PATIENT_RETURN_TO_WORK" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">USUAL WORK ACTIVITIES</asp:ListItem>
                                                <asp:ListItem Value="1">- LIMITED WORK ACTIVITIES  </asp:ListItem>
                                            </asp:RadioButtonList></td>
                                        <td class="lbl" width="50%">
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td class="lbl" width="70%">
                                            CAN PATIENT RETURN TO WORK?
                                        </td>
                                        <td class="lbl" width="30%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="70%">
                                            <asp:CheckBox ID="CHK_PATIENT_CANNOT_WORK_RET" runat="server" Text=" THE PATIENT CANNOT RETURN TO WORK BECAUSE (EXPLAIN) " /></td>
                                        <td class="lbl" width="30%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" colspan="2">
                                            <asp:TextBox ID="TXT_PATIENT_CANNOT_WORK_RET" runat="server" Text="" Width="96%"></asp:TextBox></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td width="70%" class="lbl">
                                            <asp:CheckBox ID="CHK_PATIENT_CAN_WORK_WITHOUT_LIMIT" runat="server" Text="- THE PATIENT CAN RETURN TO WORK WITHOUT LIMITATIONS ON ">
                                            </asp:CheckBox></td>
                                        <td width="30%" class="lbl">
                                            <asp:TextBox ID="TXT_PATIENT_CAN_WORK_WITHOUT_LIMIT" runat="server" Text=""></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="70%">
                                            <asp:CheckBox ID="CHK_PATIENT_CAN_WORK_WITH_LIMIT" runat="server" Text="- THE PATIENT CAN RETURN TO WORK WITH THE FOLLOWING LMIITATIONS ON  "
                                                Width="100%" /></td>
                                        <td class="lbl" width="30%" style="height: 22px">
                                            <asp:TextBox ID="TXT_PATIENT_CAN_WORK_WITH_LIMIT" runat="server" Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_BENDING_TWISTING" runat="server" Text="-BENDING/TWISTING" /></td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_LIFTING" runat="server" Text="- LIFTING" /></td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_SITTING" runat="server" Text="- SITTING" /></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_CLIMBING_STAIRS_LADDERS" runat="server"
                                                Text="- CLIMBING STAIRS/LADDERS" /></td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_OPERATING_HEAVY_EQUIP" runat="server" Text="- OPERATING HEAVY EQUIPMENT" /></td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_STANDING" runat="server" Text="- STANDING" /></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_ENVIRONMENTAL_CONDI" runat="server" Text="- ENVIRONMENTAL CONDITIONS" /></td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_OPERATION_MOTOR_VHCLE" runat="server" Text="- OPERATION OF MOTOR VEHICLES" /></td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_USE_PUBLIC_TRASPT" runat="server" Text="- USE OF PUBLIC TRANSPORTATION" /></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_KNEELING" runat="server" Text="- KNEELING" /></td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_PERSONAL_PROTIVE_EQUIP" runat="server" Text="- PERSONAL PROTECTIVE EQUIPMENT" /></td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_USE_UPPER_EXTREMITIES" runat="server" Text="- USE OF UPPER EXTREMITIES" /></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_OTHER" runat="server" Text="- OTHER " /></td>
                                        <td class="lbl" colspan="2">
                                            <asp:TextBox ID="TXT_PATIENT_WORK_LIMIT_OTHER" runat="server" Text="" Width="95%"></asp:TextBox></td>
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
                                            <asp:TextBox ID="TXT_DESCRIBE_LIMITATION" runat="server" TextMode="MultiLine" Width="100%"
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
                                            <asp:CheckBox ID="CHK_LIMIT_APPLY_1_TO_2_DAYS" runat="server" Text="- 1-2 DAYS" />
                                        </td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_LIMIT_APPLY_3_TO_7_DAYS" runat="server" Text="- 3-7 DAYS" />
                                        </td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_LIMIT_APPLY_8_TO_14_DAYS" runat="server" Text="- 8-14 DAY" />
                                        </td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_LIMIT_APPLY_15_PLUS_DAYS" runat="server" Text="- 15+ DAYS" />
                                        </td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_LIMIT_APPLY_UNKNOWN" runat="server" Text="- UNKNOWN" />
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
                                            <asp:RadioButtonList ID="RDO_DISCUSS_LIMIT_PATIENT" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">- WITH PATIENT</asp:ListItem>
                                                <asp:ListItem Value="1">- WITH PATIENT’S EMPLOYER</asp:ListItem>
                                                <asp:ListItem Value="2">- N/A</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%" class="lbl">
                                            <asp:CheckBox ID="CHK_PROVIDE_SERVICE" runat="server" Text="-I PROVIDED THE SERVICES LISTED ABOVE." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%" class="lbl">
                                            <asp:CheckBox ID="CHK_SUPERVISE_SERVICE" runat="server" Text=" - I ACTIVELY SUPERVISED THE HEALTH-CARE PROVIDER NAMED BELOW WHO PROVIDED THESE SERVICES." />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td width="5%" class="lbl">
                                            <asp:Label ID="lbl_txt_doctor_name" Text="Doctor's Name" runat="server" Width="100%"> </asp:Label>
                                        </td>
                                        <td width="20%" class="lbl">
                                            <asp:TextBox ID="TXT_DOCTOR_NAME" runat="server" Width="20%" Height="40%"> </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="5%" class="lbl">
                                            <asp:Label ID="lbl_doctor_provider_company_name" Text="Company Name" runat="server"
                                                Width="100%"> </asp:Label>
                                        </td>
                                        <td width="20%" class="lbl">
                                            <asp:TextBox ID="TXT_DOCTOR_PROVIDER_COMPANY_NAME" runat="server" Width="20%" Height="40%"> </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click" />
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Buttons" OnClick="btnSave_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        
                        
                    </tbody>
                   
                </table>
                
                 <table width="100%" visible="false">
                    <tr visible="false">
                    <td visible="false" style="height: 22px">
                    <asp:TextBox ID="txtRDO_PATIENT_MISSED_WORK" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                         <asp:TextBox ID="txtRDO_PATIENT_CURRENTLY_WORKING" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                         <asp:TextBox ID="txtRDO_PATIENT_RETURN_TO_WORK" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                         <asp:TextBox ID="txtRDO_DISCUSS_LIMIT_PATIENT" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td style="height: 22px">
                    <asp:TextBox ID="txtEventID" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    </tr>
                    </table>
            </td>
        </tr>
    </table>
</asp:Content>
