<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_FUIM_Return_To_Work.aspx.cs" Inherits="Bill_Sys_FUIM_Return_To_Work" %>

 
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"> 
     <table width="100%">
        <tr>
            <td width="100%" class="TDPart"  > 
                   <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                    <td style="width: 100%; height: 73px;">
                       <table width="100%">
                           <tr>
                               <td colspan="2" style="height: 20px">
                                   <asp:Label ID="lblError" runat="server" Text=" " Visible="False" Width="50%"></asp:Label></td>
                               <td style="width: 20%; height: 20px">
                               </td>
                               <td style="width: 20%; height: 20px">
                               </td>
                           </tr>
                            <tr>
                                <td style="width: 15%; height: 20px">
                        <asp:Label ID="lbl_patientsname" runat="server" Text=" Patient's Name " CssClass="lbl" Width="70%"></asp:Label></td>
                                <td style="width: 45%; height: 20px">
                        <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="80%" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                <td style="width: 20%; height: 20px">
                        <asp:Label ID="lbl_dateofAccident" runat="server" Text=" Date of Accident " CssClass="lbl" Width="80%"></asp:Label></td>
                                <td style="width: 20%; height: 20px">
                        <asp:TextBox ID="TXT_DOA" runat="server" CssClass="textboxCSS" Width="80%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 15%; height: 27px;">
                                    <asp:Label ID="lbl_DOS" runat="server" Text="DOS" CssClass="lbl" Width="70%"></asp:Label></td>
                                <td style="width: 45%; height: 27px;">
                                 
                               <asp:TextBox CssClass="textboxCSS" ID="TXT_DOS" runat="server" Width="30%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                <td style="width: 20%; height: 27px;">
                                </td>
                                <td style="width: 20%; height: 27px;">
                                </td>
                            </tr>
                        </table>
                        <asp:TextBox ID="TXT_EVENT_ID" runat="server" Width="5%" Visible="False"></asp:TextBox></td>
                    </tr>
                       <tr>
                           <td class="lbl" style="width: 100%; height: 19px">
                               <asp:Label ID="lbl_ReturnToWork" runat="server"
                                   Text=" RETURN TO WORK" CssClass="lbl" Width="15%"></asp:Label></td>
                       </tr>
                       <tr>
                           <td class="lbl" style="width: 100%; height: 19px">
                           </td>
                       </tr>
                       <tr>
                           <td class="lbl" style="width: 100%; height: 11px">
                               <table width=100%>
                                   <tr>
                                       <td style="width: 20%; height: 38px;">
                               
                               <asp:Label ID="lbl_ISPatientWorkingnow" runat="server" Text="IS PATIENT WORKING NOW? " CssClass="lbl" Width="100%"></asp:Label></td>
                                       <td style="width: 15%; height: 38px;">
                                           <asp:RadioButtonList ID="RDO_PATIENT_WORKING_YES" runat="server" RepeatDirection="Horizontal" Width="70%"><asp:ListItem Value="0">Yes</asp:ListItem>
<asp:ListItem Value="1">No</asp:ListItem>
</asp:RadioButtonList>
                                           <asp:TextBox ID="TXT_PATIENT_WORKING_YES" runat="server" Width="10%" Visible="False"></asp:TextBox></td>
                                       <td style="width: 30%; height: 38px;">
                            
                               <asp:Label ID="lbl_ArethrerWorkingRestrictions" runat="server" Text=" IF YES, ARE THERE WORK RESTRICTIONS ? " CssClass="lbl" Width="100%"></asp:Label></td>
                                       <td style="width: 15%; height: 38px;">
                               <asp:RadioButtonList ID="RDO_PATIENT_WORK_RESTRICT_YES" runat="server" RepeatDirection="Horizontal"
                                   Width="98px">
                                   <asp:ListItem Value="0">Yes</asp:ListItem>
                                   <asp:ListItem Value="1">No</asp:ListItem>
                               </asp:RadioButtonList>
                                           <asp:TextBox ID="TXT_PATIENT_WORK_RESTRICT_YES" runat="server" Width="10%" Visible="False"></asp:TextBox></td>
                                   </tr>
                               </table>
                               
                           </td>
                       </tr>
                       <tr>
                           <td class="ContentLabel" style="width: 100%; height: 19px">
                               <table width="100%">
                                   <tr>
                                       <td style="width: 35%" class="lbl">
                               
                               <asp:Label ID="LBL_DESCRIBE_PATIENT_WORK_RESTRICTIONS" runat="server" Text=" IF YES, DESCRIBE THE WORK RESTRICTIONS : " CssClass="lbl" Width="88%"></asp:Label>
                                       </td>
                                       <td style="width: 50%" class="lbl">
                                
                               <asp:TextBox  CssClass="textboxCSS" ID="TXT_DESCRIBE_PATIENT_WORK_RESTRICTIONS" runat="server" Width="80%"></asp:TextBox>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="width: 35%" class="lbl">
                              
                               <asp:Label ID="lbl_HowlonglimitationsApply" runat="server" Text="HOW LONG WILL THESE LIMITATIONS APPLY?  " CssClass="lbl" Width="90%"></asp:Label>
                                       </td>
                                       <td style="width: 55%" class="lbl">
                               
                               <asp:CheckBox ID="CHK_WORK_RESTRICTIONS_1_TO_2_DAYS" runat="server" Text="- 1-2 DAYS" Width="18%" CssClass="lbl" />
                               
                               <asp:CheckBox ID="CHK_WORK_RESTRICTIONS_3_TO_7_DAYS" runat="server" Text="- 3-7 DAYS" Width="19%" CssClass="lbl" />
                               
                               <asp:CheckBox ID="CHK_WORK_RESTRICTIONS_8_TO_14_DAYS" runat="server" Text="- 8-14 DAYS" Width="19%" CssClass="lbl" />
                                
                               <asp:CheckBox ID="CHK_WORK_RESTRICTIONS_15_PLUS_DAYS" runat="server" Text="- 15+ DAYS" Width="19%" CssClass="lbl" />
                               
                               <asp:CheckBox ID="CHK_WORK_RESTRICTIONS_UNKNOWN" runat="server" Text="- UNKNOWN" Width="20%" CssClass="lbl" />
                                       </td>
                                   </tr>
                               </table>
                           </td>
                                
                       </tr>
                       <tr>
                           <td class="lbl" style="width: 100%; height: 19px">
                           </td>
                       </tr>
                       <tr>
                           <td class="lbl" style="width: 100%; height: 19px">
                               <table width="100%">
                                   <tr>
                                       <td style="width: 100%">
                                <asp:Label ID="lbl_CanpatientReturnToWork" runat="server" Text=" CAN PATIENT RETURN TO WORK?" CssClass="lbl" Width="30%"></asp:Label>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="width: 100%">
                              
                               <asp:CheckBox ID="CHK_PATIENT_CANNOT_WORK_RET" runat="server" Text="- THE PATIENT CANNOT RETURN TO WORK BECAUSE (EXPLAIN)" Width="55%" CssClass="lbl" />
                                       </td>
                                   </tr>
                               </table>
                           </td>
                       </tr>
                       <tr>
                           <td class="lbl" style="width: 100%; height: 19px">
                                
                               <asp:TextBox  CssClass="textboxCSS" ID="TXT_PATIENT_CANNOT_WORK_RET" runat="server" Width="90%" Height="35px" TextMode="MultiLine"></asp:TextBox></td>
                       </tr>
                       <tr>
                           <td class="ContentLabel" style="width: 100%; height: 19px">
                               <table width="100%">
                                   <tr>
                                       <td style="width: 65%; height: 9px" class="lbl">
                               
                               <asp:CheckBox ID="CHK_PATIENT_CAN_WORK_WITHOUT_LIMIT" runat="server" Text="- THE PATIENT CAN RETURN TO WORK WITHOUT LIMITATIONS ON" Width="90%" CssClass="lbl" />
                                       </td>
                                       <td style="width: 35%; height: 9px" class="lbl">
                               
                               <asp:TextBox  CssClass="textboxCSS" ID="TXT_PATIENT_CAN_WORK_WITHOUT_LIMIT" runat="server" Width="90%"></asp:TextBox>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="width: 65%" class="lbl">
                                <asp:CheckBox ID="CHK_PATIENT_CAN_WORK_WITH_LIMIT" runat="server" Text="- THE PATIENT CAN RETURN TO WORK WITH THE FOLLOWING LMIITATIONS ON" Width="98%" CssClass="lbl" />
                                       </td>
                                       <td style="width: 35%; height: 26px" class="lbl">
                             
                               <asp:TextBox  CssClass="textboxCSS" ID="TXT_PATIENT_CAN_WORK_WITH_LIMIT" runat="server" Width="90%"></asp:TextBox>
                                       </td>
                                   </tr>
                               </table>
                           </td>
                       </tr>
                       <tr>
                           <td class="ContentLabel" style="width: 100%; height: 23px">
                               &nbsp;</td>
                       </tr>
                       <tr>
                           <td class="ContentLabel" style="width: 100%; height: 17px">
                               <table width="100%">
                                   <tr>
                                       <td style="width: 32%" class="lbl">
                               <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_BENDING_TWISTING" runat="server" Text="- BENDING/TWISTING" Width="80%" CssClass="lbl" /></td>
                                       <td style="width: 32%" class="lbl">
                               <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_LIFTING" runat="server" Text="- LIFTING" Width="80%" CssClass="lbl" />
                                       </td>
                                       <td style="width: 32%" class="lbl">
                               <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_SITTING" runat="server" Text="- SITTING" Width="80%" CssClass="lbl" /></td>
                                   </tr>
                                   <tr>
                                       <td style="width: 32%" class="lbl">
                               <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_CLIMBING_STAIRS_LADDERS" runat="server" Text="- CLIMBING STAIRS/LADDERS" Width="80%" CssClass="lbl" /></td>
                                       <td style="width: 32%" class="lbl">
                               <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_OPERATING_HEAVY_EQUIP" runat="server" Text="- OPERATING HEAVY EQUIPMENT" Width="90%" CssClass="lbl" /></td>
                                       <td style="width: 32%" class="lbl">
                               <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_STANDING" runat="server" Text="- STANDING" Width="80%" CssClass="lbl" /></td>
                                   </tr>
                                   <tr>
                                       <td style="width: 32%;" class="lbl">
                               <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_ENVIRONMENTAL_CONDI" runat="server" Text="- ENVIRONMENTAL CONDITIONS" Width="90%" CssClass="lbl" /></td>
                                       <td style="width: 32%;" class="lbl">
                               <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_OPERATION_MOTOR_VHCLE" runat="server" Text="- OPERATION OF MOTOR VEHICLES" Width="90%" CssClass="lbl" /></td>
                                       <td style="width: 32%;" class="lbl">
                               <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_USE_PUBLIC_TRASPT" runat="server" Text="- USE OF PUBLIC TRANSPORTATION" Width="90%" CssClass="lbl" /></td>
                                   </tr>
                                   <tr>
                                       <td style="width: 32%" class="lbl">
                               <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_KNEELING" runat="server" Text="- KNEELING" Width="80%" CssClass="lbl" /></td>
                                       <td style="width: 32%" class="lbl">
                               <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_PERSONAL_PROTIVE_EQUIP" runat="server" Text="- PERSONAL PROTECTIVE EQUIPMENT" Width="90%" CssClass="lbl" /></td>
                                       <td style="width: 32%" class="lbl">
                               <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_USE_UPPER_EXTREMITIES" runat="server" Text="- USE OF UPPER EXTREMITIES" Width="80%" CssClass="lbl" /></td>
                                   </tr>
                                   <tr>
                                       <td colspan="3">
                                           <table width="100%">
                                               <tr>
                                                   <td style="width: 15%; height: 40px;" class="lbl">
                               <asp:CheckBox ID="CHK_PATIENT_WORK_LIMIT_OTHER" runat="server" Text="- OTHER" Width="60%" CssClass="lbl" />
                                                   </td>
                                                   <td style="width: 80%; height: 40px;" class="lbl">
                               
                               <asp:TextBox CssClass="textboxCSS" ID="TXT_PATIENT_WORK_LIMIT_OTHER" runat="server" Width="80%" TextMode="MultiLine"></asp:TextBox>
                                                   </td>
                                               </tr>
                                           </table>
                                       </td>
                                   </tr>
                               </table>
                               </td>
                       </tr>
                       <tr>
                           <td class="lbl" style="width: 100%; height: 19px">
                               
                               <asp:Label ID="lbl_Describelimitations" runat="server" Text="DESCRIBE / QUANTIFY THE LIMITATIONS: " CssClass="lbl" Width="40%"></asp:Label></td>
                       </tr>
                       <tr>
                           <td class="ContentLabel" style="width: 100%; height: 19px">
                           </td>
                       </tr>
                       <tr>
                           <td class="lbl" style="width: 100%; height: 14px">
                               
                               <asp:TextBox CssClass="textboxCSS" ID="TXT_DESCRIBE_LIMITATION" runat="server" Width="80%" TextMode="MultiLine"></asp:TextBox></td>
                       </tr>
                       <tr>
                           <td class="lbl" style="width: 100%; height: 27px">
                               <table width="100%">
                                   <tr>
                                       <td style="width: 36%; height: 22px;">
                                
                               <asp:Label ID="lbl_Howlonglimitationsaplly" runat="server" Text="HOW LONG WILL THESE LIMITATIONS APPLY? " CssClass="lbl" Width="100%"></asp:Label>
                                       </td>
                                       <td style="width: 11%; height: 22px;">
                                
                               <asp:CheckBox ID="CHK_LIMIT_APPLY_1_TO_2_DAYS" runat="server" Text="- 1-2 DAYS" Width="100%" CssClass="lbl" />
                                       </td>
                                       <td style="width: 14%; height: 22px;">
                               
                               <asp:CheckBox ID="CHK_LIMIT_APPLY_8_TO_14_DAYS" runat="server" Text="- 8-14 DAYS" Width="90%" CssClass="lbl" />
                                       </td>
                                       <td style="width: 11%; height: 22px;">
                                
                               <asp:CheckBox ID="CHK_LIMIT_APPLY_3_TO_7_DAYS" runat="server" Text="- 3-7 DAYS" Width="100%" CssClass="lbl" />
                                       </td>
                                       <td style="width: 11%; height: 22px;">
                                
                               <asp:CheckBox ID="CHK_LIMIT_APPLY_15_PLUS_DAYS" runat="server" Text="- 15+ DAYS" Width="100%" CssClass="lbl" />
                                       </td>
                                       <td style="width: 15%; height: 22px;">
                                
                               <asp:CheckBox ID="CHK_LIMIT_APPLY_UNKNOWN" runat="server" Text="- UNKNOWN" Width="100%" CssClass="lbl" />
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="width: 36%">
                                       </td>
                                       <td style="width: 11%">
                                       </td>
                                       <td style="width: 14%">
                                       </td>
                                       <td style="width: 11%">
                                       </td>
                                       <td style="width: 11%">
                                       </td>
                                       <td style="width: 15%">
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="width: 36%">
                                       </td>
                                       <td style="width: 11%">
                                       </td>
                                       <td style="width: 14%">
                                       </td>
                                       <td style="width: 11%">
                                       </td>
                                       <td style="width: 11%">
                                       </td>
                                       <td style="width: 15%">
                                       </td>
                                   </tr>
                               </table>
                                
                               <asp:Label ID="lbl_WithWhomDiscusspatientsReturning" runat="server" Text=" WITH WHOM WILL YOU DISCUSS THE PATIENT’S RETURNING TO WORK AND /OR LIMITATION?" CssClass="lbl" Width="80%"></asp:Label></td>
                       </tr>
                       <tr>
                           <td class="lbl" style="width: 1242px; height: 19px">
                               <asp:RadioButtonList ID="RDO_DISCUSS_LIMIT_PATIENT" runat="server" RepeatDirection="Horizontal" Width="50%"><asp:ListItem Value="0">- WITH PATIENT</asp:ListItem>
<asp:ListItem Value="1">-WITH PATIENT’S EMPLOYER</asp:ListItem>
<asp:ListItem Value="2">- N/A</asp:ListItem>
</asp:RadioButtonList>
                               <asp:TextBox ID="TXT_DISCUSS_LIMIT_PATIENT" runat="server" Width="5%" Visible="False"></asp:TextBox></td>
                       </tr>
                       <tr>
                           <td class="ContentLabel" style="width: 100%; height: 20px">
                               &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                               <table width=100%>
                                   <tr>
                                       <td style="width: 60%" class="lbl">
                              <asp:Label ID="lbl_PatientBenefitFromrehabilitation"
                                   runat="server" Text="WOULD THE PATIENT BENEFIT FROM VOCATIONAL REHABILITATION  " CssClass="lbl" Width="90%"></asp:Label></td>
                                       <td style="width: 40%" class="lbl">
                                           <asp:RadioButtonList ID="RDO_PATIENT_BENEFIT_REHABILITATION" runat="server"
                                   RepeatDirection="Horizontal" Width="20%"><asp:ListItem Value="0">Yes</asp:ListItem>
<asp:ListItem Value="1">No</asp:ListItem>
</asp:RadioButtonList>
                                           <asp:TextBox ID="TXT_PATIENT_BENEFIT_REHABILITATION" runat="server" Width="10%" Visible="False"></asp:TextBox></td>
                                   </tr>
                                   <tr>
                                       <td style="width: 60%">
                                       </td>
                                       <td style="width: 40%">
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="width: 60%" class="lbl">
                              
                               <asp:CheckBox ID="CHK_PROVIDE_SERVICE" runat="server" Text="- I PROVIDED THE SERVICES LISTED ABOVE." Width="80%" CssClass="lbl" />
                                       </td>
                                       <td style="width: 40%">
                                       </td>
                                   </tr>
                                   <tr>
                                       <td colspan="2" class="lbl">
                                
                               <asp:CheckBox ID="CHK_SUPERVISE_SERVICE" runat="server" Text="- I ACTIVELY SUPERVISED THE HEALTH-CARE PROVIDER NAMED BELOW WHO PROVIDED THESE SERVICES." Width="90%" CssClass="lbl" />
                                       </td>
                                   </tr>
                               </table>
                              
                           </td>
                       </tr>
                       <tr>
                           <td class="ContentLabel" style="width: 100%; height: 20px">
                           </td>
                       </tr>
                       <tr>
                           <td class="ContentLabel" style="width: 100%; height: 20px">
                                
                           </td>
                       </tr>
                       <tr>
                           <td class="lbl" style="width: 100%; height: 20px">
                                
                               <asp:Label ID="lbl_Sincerely" runat="server" Text="SINCERELY," CssClass="lbl" Width="15%"></asp:Label></td>
                       </tr>
                       <tr>
                           <td class="ContentLabel" style="width: 100%; height: 20px">
                           </td>
                       </tr>
                       <tr>
                           <td class="ContentLabel" style="width: 100%; height: 20px">
                           </td>
                       </tr>
                       <tr>
                           <td class="lbl" style="width: 100%; height: 20px">
                                
                               <asp:Label ID="lbl_boardAuthority" runat="server" Text="  Board Authorized Health Care Provider " CssClass="lbl" Width="40%"></asp:Label></td>
                       </tr>
                    </table>
        <table width="100%">
            <tr>
                <td style="width: 39%">
                </td>
                <td style="width: 11%">
                    <asp:Button ID="BTN_PREVIOUS" runat="server" Text="Previous" Width="88%" OnClick="BTN_PREVIOUS_Click" CssClass="Buttons" /></td>
                <td style="width: 25%">
                    <asp:Button ID="BTN_SAVE" runat="server" Text="Save & Next" Width="44%" CssClass="Buttons" OnClick="BTN_SAVE_Click" /></td>
                <td style="width: 25%">
                    </td>
            </tr>
        </table>
        </td> 
         </tr>
        </table>
                    </asp:Content>
              
