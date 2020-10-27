<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Bill_Sys_FUIM_StartExamination.aspx.cs" Inherits="Bill_Sys_FUIM_StartExamination" %>

 
 <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <table width="100%">
 <tr>
 <td width="100%"  class="TDPart">
 <table width="100%">
 
 </table>
<%--<table width="100%">
    
    <tr>
        <td class="lbl"   style="width: 30%">
            <asp:Label ID="LBL_ProviderName" runat="server" Text="PROVIDER NAME" Font-Size="Small" Width="80%"></asp:Label></td>
        <td class="lbl" style="width: 48%">
            &nbsp;<asp:TextBox ID="TXT_PROVIDER_NAME" runat="server" Width="89%" CssClass="textboxCSS" Height="14px"></asp:TextBox></td>
        <td class="lbl" style="width: 20%"></td>
    </tr>
    <tr>
        <td class="lbl" style="width: 30%">
            
            <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="PROVIDER ADDRESS"
                Width="80%"></asp:Label></td>
        <td class="lbl" style="width: 48%">
        <asp:textbox id="TXT_PROVIDER_ADDRESS" runat="server" width="90%" CssClass="textboxCSS"></asp:textbox></td>
        <td class="lbl" style="width: 20%">
            &nbsp;Tel:  
             <asp:textbox id="TXT_PROVIDER_PHONE" runat="server" width="71%" CssClass="textboxCSS"></asp:textbox></td>
    </tr>
    <tr>
        <td class="lbl" colspan="2" style="width: 50%"  >
            &nbsp;<asp:Label ID="Label3" runat="server" Font-Size="Small" Text="CITY"
                 Width="32px"></asp:Label>
            <asp:textbox id="TXT_PROVIDER_CITY" runat="server" width="15%" CssClass="textboxCSS"></asp:textbox>
             <asp:Label ID="Label4" runat="server" Font-Size="Small" Text="STATE" Width="49px"></asp:Label>
            <asp:textbox id="TXT_PROVIDER_STATE" runat="server" width="7%" CssClass="textboxCSS"></asp:textbox>
             <asp:Label ID="Label5" runat="server" Font-Size="Small" Text="ZIP" Width="31px"></asp:Label>
            <asp:textbox id="TXT_PROVIDER_ZIP" runat="server" width="15%" CssClass="textboxCSS"></asp:textbox></td>
               <td class="lbl" colspan="2" style="width: 50%"  >
                   &nbsp;Fax:<asp:textbox id="TXT_PROVIDER_FAX" runat="server" width="71%" CssClass="textboxCSS"></asp:textbox></td>
         
    </tr>
     <tr>
         <td class="lbl" colspan="2">
             &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
         </td>
         <td style="width:20%" class="lbl">
             &nbsp;
         </td>
    </tr>
    
    <tr>
       
    </tr>
</table>--%>
  <table width="100%">
                    <tr>
                        <td class="lbl" style="width: 20%">
                            Patient's &nbsp;Name</td>
                        <td style="width: 41%" class="lbl">
                            <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="90%" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="lbl" style="width: 17%">
                            Date Of Accident</td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="TXT_DOA" runat="server" Width="70%" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 20%" >
                            Date Of Examination</td>
                        <td style="width: 41%" class="lbl">
                            <asp:TextBox ID="TXT_DOE" runat="server" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                           <%-- <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" / >
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender15" runat="server" TargetControlID="txtDOE"
                                PopupButtonID="imgbtnFromDate" />--%>
                        </td>
                        <td class="lbl" style="width: 17%">
                            Date Of Birth</td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="TXT_DOB" runat="server" Width="70%" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                           <%-- <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDOB"
                                PopupButtonID="imgbtnFromDate" />--%>
                        </td>
                    </tr>
                </table>
<table style="width: 100%">
    <tr>
        <td STYLE="WIDTH:100%">
        </td>
    </tr>
    <tr>
        <td STYLE="WIDTH:100%" class="lbl">
        <B>LIST ANY CHANGES REVEALED BY YOUR MOST RECENT EXAMINATION IN THE FOLLOWING:</B>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
        </td>
    </tr>
    <tr>
        <td STYLE="WIDTH:100%" class="lbl">
        AREA OF INJURY , TYPE/NATURE OF INJURY ,PATIENTS SUBJECTED COMPLAINTS OR YOUR OBJECTIVE FINDINGS:
        </td>
    </tr>
    <tr>
        <td STYLE="WIDTH:100%" class="lbl">
            <asp:textbox id="TXT_REVEALED_BY_RECENT_EXAMINATION" runat="server" width="90%" CssClass="textboxCSS"></asp:textbox>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
        </td>
    </tr>
    <tr>
        <td STYLE="WIDTH:100%; height: 13px;" class="lbl">
        LIST ADDITIONAL BODY PARTS ARRIVED BY THIS INJURY , IF ANY:
        </td>
    </tr>
    <tr>
        <td style="width: 100%; height: 21px" class="lbl">
            <asp:textbox id="TXT_ADDITIONAL_BODY_PARTS_AFFECTED" runat="server" width="90%" CssClass="textboxCSS"></asp:textbox>
        </td>
    </tr>
    <tr>
        <td style="width: 100%; height: 21px">
        </td>
    </tr>
</table>
<table width="100%" class="lbl">
                            <tr>
                                <td style="width: 16%">
        <asp:CheckBox ID="CHK_PATIENT_ARRIVED_BUS" runat="server" Text="Bus" Width="70%" CssClass="lbl" /></td>
                                <td style="width: 31%">
        <asp:CheckBox ID="CHK_PATIENT_ARRIVED_SUBWAY" runat="server" Text="SUBWAY" Width="70%" CssClass="lbl" /></td>
                                <td style="width: 16%">
        <asp:CheckBox ID="CHK_PATIENT_ARRIVED_TAXI" runat="server" Text="Taxi" Width="70%" CssClass="lbl" /></td>
                                <td style="width: 11%">
                                    <asp:CheckBox ID="CHK_PATIENT_ARRIVED_WALKING" runat="server" Text="Walking"
            Width="77%" CssClass="lbl" /></td>
                                <td style="width: 12%">
        <asp:CheckBox ID="CHK_PATIENT_ARRIVED_AMBULATE" runat="server" Text="Ambulate" Width="81%" CssClass="lbl" /></td>
                                <td style="width: 16%">
                                    <asp:CheckBox ID="CHK_PATIENT_ARRIVED_PRIVATE_CAR" runat="server" Text="Private Car" Width="100%" CssClass="lbl" /></td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
        <asp:CheckBox ID="CHK_PATIENT_ARRIVED_OTHER" runat="server" Text="Other" Width="90%" CssClass="lbl" /></td>
                                <td style="width: 31%">
        <asp:TextBox ID="TXT_PATIENT_OTHER_TRANSPORT" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox></td>
                            </tr>
    <tr>
        <td style="width: 10%">
        </td>
        <td style="width: 31%">
        </td>
    </tr>
                        </table>
                        
                        <table width="100%">
            <tr>
                <td style="width: 50%" class="lbl">
        <asp:Label ID="lbl_DescribeTreatment" runat="server" Text="DESCRIBE TREATMENT RENDERED TODAY:" Width="94%" CssClass="lbl"></asp:Label></td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 50%; height: 28px" class="lbl">
        <asp:CheckBox ID="CHK_TREATMENT_99211" runat="server" Text="99211 – Follow up" Height="25px" Width="100%" CssClass="lbl" /></td>
                <td style="width: 50%; height: 28px" class="lbl">
                    <asp:CheckBox ID="CHK_TREATMENT_99214"
            runat="server" Text="99214 – Follow up" Height="26px" Width="100%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 50%; height: 26px" class="lbl">
        <asp:CheckBox ID="CHK_TREATMENT_99212" runat="server" Text="99212 – Follow up" Width="100%" CssClass="lbl" /></td>
                <td style="width: 50%; height: 26px" class="lbl">
        <asp:CheckBox ID="CHK_TREATMENT_99215" runat="server" Text="99215 – Follow up" Width="100%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 50%" class="lbl">
        <asp:CheckBox ID="CHK_TREATMENT_99213" runat="server" Text="99213 – Follow up" Width="100%" CssClass="lbl" /></td>
                <td style="width: 50%" class="lbl">
                </td>
            </tr>
        </table>
         
        
        <table width="100%">
            <tr>
                <td style="width: 50%; height: 21px" class="lbl">
        <asp:Label ID="lbl_CurrentComplaints" runat="server" Font-Size="Large" Font-Underline="True"
            Text="CURRENT COMPLAINTS:" CssClass="lbl" Width="95%"></asp:Label></td>
                <td style="width: 50%; height: 21px" class="lbl">
                </td>
            </tr>
            <tr>
                <td style="width: 50%" class="lbl">
        <asp:CheckBox ID="CHK_HEADACHES" runat="server" Text="HEADACHES" Width="50%" CssClass="lbl" /></td>
                <td style="width: 50%" class="lbl">
                    <asp:CheckBox
            ID="CHK_KNEE_PAIN" runat="server" Text="RT / LT KNEE PAIN" Width="50%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 50%;" class="lbl">
        <asp:CheckBox ID="CHK_DIZZINESS" runat="server" Text="DIZZINESS" Width="50%" CssClass="lbl" /></td>
                <td style="width: 50%;" class="lbl">
                    <asp:CheckBox ID="CHK_SHOULDER_PAIN" runat="server" Text="RT / LT SHOULDER PAIN" Width="50%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 50%" class="lbl">
        <asp:CheckBox ID="CHK_JAW_PAIN" runat="server" Text="JAW PAIN" Width="106px" CssClass="lbl" /></td>
                <td style="width: 50%" class="lbl">
                    <asp:CheckBox ID="CHK_LOWER_BACK_PAIN_STIFFNESS" runat="server" Text="LOWER BACK PAIN & STIFFNESS" Width="54%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 50%" class="lbl">
                    <asp:CheckBox ID="CHK_NECK_PAIN_STIFFNESS" runat="server" Text="NECK PAIN AND STIFFNESS" Width="50%" CssClass="lbl" /></td>
                <td style="width: 50%" class="lbl">
                    <asp:CheckBox ID="CHK_UPPER_BACK_PAIN_STIFFNESS"
            runat="server" Text="UPPER BACK PAIN & STIFFNESS" Width="53%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 341px" class="lbl">
        <asp:CheckBox ID="CHK_CHEST_PAIN" runat="server" Text="CHEST PAIN" Width="50%" CssClass="lbl" /></td>
                <td style="width: 50%" class="lbl">
                    <asp:CheckBox
            ID="CHK_UNABLE_TO_LIFT_OBJECTS" runat="server" Text="UNABLE TO LIFT HEAVY OBJECTS" Width="56%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 341px; height: 21px" class="lbl">
        <asp:CheckBox ID="CHK_DIFFICULTY_IN_BREATHING" runat="server" Text="DIFFICULTY IN BREATHING" Width="59%" CssClass="lbl" /></td>
                <td style="width: 50%;" class="lbl">
        <asp:CheckBox ID="CHK_SHOOTING_PAIN_LEG" runat="server" Text="SHOOTING PAIN DOWN IN THE RT / LT LEG" Width="67%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 341px; height: 4px;" class="lbl">
        <asp:CheckBox ID="CHK_RESTRICTION_NECK_MOTION" runat="server" Text="RESTRICTION OF NECK MOTION" Width="274px" CssClass="lbl" /></td>
                <td style="width: 50%;" class="lbl">
        <asp:CheckBox ID="CHK_DIFFICULT_TO_WALK_AFTER_SITTING" runat="server" Text="DIFFICULTY IN RISING TO WALK AFTER SITTING" Width="74%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 50%" class="lbl">
                    <asp:CheckBox ID="CHK_NUMBNESS_TINGLING_TOES" runat="server" Text="NUMBNESS / TINGLING IN FINGERS / TOES" Width="66%" CssClass="lbl" /></td>
                <td style="width: 485px" class="lbl">
                    <asp:CheckBox ID="CHK_DIFFICULT_IN_PROLONGED_RIDING"
            runat="server" Text="DIFFICULTY IN PROLONGED RIDING IN AN AUTOMOBILE" Width="86%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 50%" class="lbl">
        <asp:CheckBox ID="CHK_ANXIETY_STRESS" runat="server" Text="ANXIETY / STRESS" Width="50%" CssClass="lbl" /></td>
                <td style="width: 50%" class="lbl">
                    <asp:CheckBox ID="CHK_INSOMNIA" runat="server"
            Text="INSOMNIA" Width="50%" CssClass="lbl"   /></td>
            </tr>
            <tr>
                <td style="width: 50%" class="lbl">
        <asp:CheckBox ID="CHK_VISUAL_PROBLEMS" runat="server" Text="VISUAL PROBLEMS" Width="50%" CssClass="lbl" /></td>
                <td style="width: 50%" class="lbl">
                    <asp:CheckBox ID="CHK_MEMORY_PROBLEMS" runat="server"
            Text="MEMORY PROBLEMS" Width="50%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 50%" class="lbl">
        <asp:CheckBox ID="CHK_FATIGUE" runat="server" Text="FATIGUE" Width="50%" CssClass="lbl" /></td>
                <td style="width: 50%" class="lbl">
        <asp:CheckBox ID="CHK_CONCENTRATION_PROBLEMS" runat="server" Text="CONCENTRATION PROBLEMS" Width="50%" CssClass="lbl" /></td>
            </tr>
        </table>
     <table width="100%">
         <tr>
             <td style="width: 25%">
                 <asp:TextBox ID="TXT_EVENT_ID" runat="server" Visible="False" Width="10%"></asp:TextBox></td>
             <td style="width: 8%">
             </td>
             <td style="width: 19%">
                 <asp:Button ID="BTN_SAVE_NEXT" runat="server" Text="Save & Next" Width="58%" OnClick="BTN_SAVE_NEXT_Click" CssClass="Buttons" /></td>
             <td style="width: 25%">
                 </td>
         </tr>
     </table>
      </td>
 </tr>
 </table>
 <%-- <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDOB"
                                PopupButtonID="imgbtnFromDate" />--%>
        </asp:Content>
    
                    