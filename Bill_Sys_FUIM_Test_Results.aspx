<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_FUIM_Test_Results.aspx.cs" Inherits="Bill_Sys_FUIM_Test_Results" %>
 
 
 <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <table width="100%">
   <tr>
   <td  width="100%" class="TDPart">
     <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%">
                   <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                    <td>
                        <asp:TextBox ID="TXT_EVENT_ID" runat="server" Width="3%" Visible="False"></asp:TextBox><br />
                        <table width="100%">
                            <tr>
                                <td style="width: 18%">
        <asp:Label ID="lbl_patientname" runat="server" Text="Patient's Name" Width="80%" CssClass="lbl" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:Label></td>
                                <td style="width: 40%">
        <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="80%" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:TextBox></td>
                                <td style="width: 13%">
        <asp:Label ID="lbl_DateOfAccident" runat="server" Text="Date of Accident" CssClass="lbl" Width="82%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:Label></td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="TXT_DOA" runat="server" CssClass="textboxCSS" Width="71%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 18%">
        <asp:Label ID="lbl_DOS" runat="server" Text="DOS" CssClass="lbl" Width="50%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:Label></td>
                                <td style="width: 40%">
        <asp:TextBox ID="TXT_DOS" runat="server" CssClass="textboxCSS" Width="30%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                <td style="width: 13%">
                                </td>
                                <td style="width: 20%">
                                </td>
                            </tr>
                        </table>
                        
        <br />
        <table width="100%">
            <tr>
                <td style="width: 55%" class="lbl">
        <asp:Label ID="lbl_testResults" runat="server" Font-Size="Large" Font-Underline="True"
            Text="TESTS RESULTS DISCUSSED WITH PATIENT" Width="95%" CssClass="lbl"></asp:Label></td>
                <td style="width: 10%" class="lbl">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 35%" class="lbl">
                </td>
            </tr>
            <tr>
                <td style="width: 45%"  class="lbl"  >
        <asp:CheckBox ID="CHK_X_RAY_CER_SPINE_DEMO" runat="server" Text="X-RAY OF THE CERVICAL SPINE DEMONSTRATES" Width="95%" CssClass="lbl"  /></td>
                <td style="width: 10%"  class="lbl"  >
                </td>
                <td style="width: 10%"  class="lbl">
                </td>
                <td style="width: 40%" class="lbl"  >
        <asp:TextBox ID="TXT_X_RAY_CER_SPINE_DEMO" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 45%"  class="lbl"  >
        <asp:CheckBox ID="CHK_X_RAY_LUM_SPINE_DEMO" runat="server" Text="X-RAY OF THE LUMBAR SPINE DEMONSTRATES" Width="96%" CssClass="lbl"   /></td>
                <td style="width: 10%"  class="lbl"  >
                </td>
                <td style="width: 10%"  class="lbl">
                </td>
                <td style="width: 40%" class="lbl">
                    <asp:TextBox ID="TXT_X_RAY_LUM_SPINE_DEMO" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 45%"  class="lbl"  >
        <asp:CheckBox ID="CHK_X_RAY__DEMO" runat="server" Text="X-RAY OF THE" Width="95%" CssClass="lbl"  /></td>
                <td style="width: 10%" class="lbl">
        <asp:TextBox ID="TXT_X_RAY_PART_DEMO" runat="server" Width="92%" CssClass="textboxCSS"></asp:TextBox></td>
                <td style="width: 10%" class="lbl">
                    <asp:Label
            ID="lbl_Demonstrate" runat="server" Text="DEMONSTRATES" Height="14px" Width="1%" CssClass="lbl"></asp:Label></td>
                <td style="width: 40%" class="lbl">
        <asp:TextBox ID="TXT_X_RAY_DEMO" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 45%"  class="lbl"  >
        <asp:CheckBox ID="CHK_MRI_CT_CER_SPINE_DEMO" runat="server" Text="MRI/CT OF THE CERVICAL SPINE DEMONSTRATES" Width="94%" CssClass="lbl"  /></td>
                <td style="width: 10%" class="lbl">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 40%" class="lbl">
        <asp:TextBox ID="TXT_MRI_CT_CER_SPINE_DEMO" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 45%"  class="lbl"  >
        <asp:CheckBox ID="CHK_MRI_CT_LUM_SPINE_DEMO" runat="server" Text="MRI/CT OF THE LUMBAR SPINE DEMONSTRATES" Width="95%" CssClass="lbl"   /></td>
                <td style="width: 10%" class="lbl">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 40%" class="lbl">
        <asp:TextBox ID="TXT_MRI_CT_LUM_SPINE_DEMO" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 45%;"  class="lbl"  >
        <asp:CheckBox ID="CHK_MRI_CT_DEMO" runat="server" Text="MRI/CT OF THE" Width="95%" CssClass="lbl"   /></td>
                <td style="width: 10%; height: 26px;">
        <asp:TextBox ID="TXT_MRI_CT_BODY_DEMO" runat="server" CssClass="textboxCSS" Width="91%"></asp:TextBox></td>
                <td style="width: 10%; height: 26px;" class="lbl">
        <asp:Label ID="lbl_demonstrates1" runat="server" Text="DEMONSTRATES" Width="100%" CssClass="lbl"></asp:Label></td>
                <td style="width: 40%;" class="lbl">
                    <asp:TextBox ID="TXT_MRI_CT_DEMO" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 45%;"  class="lbl"  >
        <asp:CheckBox ID="CHK_EMG_UPP_EXET_DEMO" runat="server" Text="EMG/NCV TEST OF UPPER EXTREMITIES DEMONSTRATES" Width="95%" CssClass="lbl"   /></td>
                <td style="width: 10%;" class="lbl">
                </td>
                <td style="width: 10%;">
                </td>
                <td style="width: 40%;" class="lbl">
                    <asp:TextBox ID="TXT_EMG_UPP_EXET_DEMO" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 45%"  class="lbl"  >
        <asp:CheckBox ID="CHK_EMG_LOW_EXET_DEMO" runat="server" Text="EMG/NCV TEST OF LOWER EXTREMITIES DEMONSTRATES" Width="95%" CssClass="lbl"   /></td>
                <td style="width: 10%" class="lbl">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 40%" class="lbl">
                    <asp:TextBox ID="TXT_EMG_LOW_EXET_DEMO" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 45%"  class="lbl"  >
        <asp:CheckBox ID="CHK_ROM_DEMO_TT_IMP" runat="server" Text="ROM TESTING DEMONSTRATES TOTAL IMPAIRMENT" Width="95%" CssClass="lbl"   /></td>
                <td style="width: 10%" class="lbl">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 40%" class="lbl">
                    <asp:TextBox ID="TXT_ROM_DEMO_TT_IMP" runat="server" Width="80%" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 45%">
                </td>
                <td style="width: 10%" class="lbl">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 40%">
                </td>
            </tr>
            <tr>
                <td style="width: 45%">
                </td>
                <td style="width: 10%" class="lbl">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 40%">
                </td>
            </tr>
        </table>
                         
        <table  id="tblDiagCode" runat="server" style="width: 100%">
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:Label ID="lbl_dignosticimpression" runat="server" Font-Size="Large" Font-Underline="True"
            Text="DIAGNOSTIC IMPRESSION" Width="70%" CssClass="lbl"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 100%;" class="lbl">
        <asp:CheckBox ID="chk301" runat="server" Text="- (920.0) SCALP CONTUSION" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk302" runat="server" Text="- (784.0) HEADACHES" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk303" runat="server" Text="- (780.4) DIZZINESS" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk304" runat="server" Text="- (850.0) CONCUSSION WITHOUT LOSS OF CONCIOUSNESS" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk305" runat="server" Text="- (850.1) CONCUSSION WITH BRIEF LOSS OF CONCIOUSNESS" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
                    <asp:CheckBox ID="chk306" runat="server" Text="- (847.0) CERVICAL SPRAIN/STRAIN" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk307" runat="server" Text="- (723.4) CERVICAL RADICULITIS" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk308" runat="server" Text="- (722.0) CERVICAL DISC DISPLACEMENT" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk309" runat="server" Text="- (847.1) THORACIC SPRAIN/STRAIN" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk310" runat="server" Text="- (847.2) LUMBAR SPRAIN/STRAIN" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk311" runat="server" Text="- (846.0) LUMBASACRAL SPRAIN/STRAIN" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk312" runat="server" Text="- (724.4) R/O LUMBAR RADICULITIS" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk313" runat="server" Text="- (722.1) R/O LUMBAR DISC DISPLACEMENT" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk314" runat="server" Text="- (923.0) RIGHT/LEFT SHOULDER CONTUSION" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk315" runat="server" Text="- (840.9) RIGHT/LEFT SHOULDER SPRAIN/STRAIN" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk316" runat="server" Text="- (718.31) R/O INTERNAL DERANGEMENT, SHOULDER" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk317" runat="server" Text="- (840.6) R/O TEAR SUPRASPINATUS MUSCLE" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
                    <asp:CheckBox ID="chk318" runat="server" Text="- (924.11) RIGHT/LEFT KNEE CONTUSION" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
                    <asp:CheckBox ID="chk319" runat="server" Text="- (844.1 / 844.0 / 844.2) KNEE MCL / LCL/ ACL STRAIN" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
                    <asp:CheckBox ID="chk320" runat="server" Text="- (718.36) R/O KNEE INTERNAL DERANGEMENT" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk321" runat="server" Text="- (836.0 / 836.1) R/O MEDIAL / LATERAL MENISCUS TEAR" Width="70%" CssClass="lbl" /></td>
            </tr>
         
            <tr>
                <td style="width: 100%;" class="lbl">
        <asp:CheckBox ID="chk322" runat="server" Text="- (922.1) CONTUSION OF THE CHEST WALL" Height="20px" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk323" runat="server" Text="- (845.0) RIGHT / LEFT ANKLE SPRAIN / STRAIN" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk324" runat="server" Text="- (924.21) RIGHT / LEFT ANKLE CONTUSION" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk325" runat="server" Text="- (841.9) RIGHT / LEFT ELBOW SPRAIN / STRAIN" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk326" runat="server" Text="- (923.11) RIGHT / LEFT ELBOW CONTUSION" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
                    <asp:CheckBox ID="chk327" runat="server" Text="- (845.1) RIGHT / LEFT FOOT SPRAIN / STRAIN" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk328" runat="server" Text="- (924.2) RIGHT / LEFT FOOT CONTUSION" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk329" runat="server" Text="- (843.9) RIGHT / LEFT HIP / THIGH SPRAIN / STRAIN" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk330" runat="server" Text="- (924.01) RIGHT / LEFT HIP CONTUSION" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk331" runat="server" Text="- (842.00) RIGHT / LEFT WRIST SPRAIN / STRAIN" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%" class="lbl">
        <asp:CheckBox ID="chk332" runat="server" Text="- (842.1) RIGHT / LEFT HAND SPRAIN / STRAIN" Width="70%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 100%;" class="lbl">
        <asp:CheckBox ID="chk333" runat="server" Text="- (736.32) RIGHT / LEFT ELBOW EPICONDYLITIS" Width="70%" CssClass="lbl" /></td>
            </tr>
        </table>
                        <table width="100%">
                            <tr>
                                <td style="width: 40%">
                                </td>
                                <td style="width: 12%">
                                    <asp:Button ID="BTN_PREVIOUS" runat="server" Text="Previous" Width="82%" OnClick="BTN_PREVIOUS_Click" CssClass="Buttons" /></td>
                                <td style="width: 25%">
                                    <asp:Button ID="BTN_SAVE_NEXT" runat="server" Text="Save & Next" Width="43%" OnClick="BTN_SAVE_NEXT_Click" CssClass="Buttons" /></td>
                                <td style="width: 25%">
                                    </td>
                            </tr>
                        </table>
        <br />
        <br />       
          </td>
                       </tr>
                    </table>
                </td> 
                </tr>
                </table> 
                </td>
                </tr>
                </table>
                </asp:Content>
    
