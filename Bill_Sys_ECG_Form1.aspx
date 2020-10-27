<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_ECG_Form1.aspx.cs" Inherits="Bill_Sys_ECG_Form1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <table width="100%">
 <tr>
 <td width="100%"  class="TDPart">
   <table width="100%">
    
    <tr>
            <td style="width: 20%" align="center" class="lbl">
                Patient's Name:</td>
            <td style="width: 30%" align="left" class="lbl"> <asp:TextBox ID="TXT_PATIENT_NAME" runat="server"   CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
            <td style="width: 20%" align="center" class="lbl">
                DOA:</td>
            <td style="width: 30%" align="center" class="lbl"> <asp:TextBox ID="TXT_DOA" runat="server"   CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
           
            
       </tr>
       <tr>
            <td style="width: 20%" align="center" class="lbl">Date Of Service:</td>
            <td style="width: 30%" align="left" class="lbl"> <asp:TextBox ID="TXT_DATE_OF_SERVICE" runat="server" ReadOnly="True" CssClass="textboxCSS"></asp:TextBox></td>
       </tr>
        <tr>
            <td style="width: 20%" align="center" class="lbl">Referring Doctor:</td>
            <td style="width: 30%" align="left"> <asp:TextBox ID="TXT_REFERRING_DOCTOR" runat="server" Width="286px" CssClass="textboxCSS"></asp:TextBox></td>
       </tr>
       </table>
        <table width="100%">
       <tr>
       <td style="width:50%" align="left" class="lbl"><asp:CheckBox ID="CHK_CERVICAL_NERVE_EVALUATION" runat="server" Text="Cervical/Thoracic Nerves Evaluation" /></td>
            <td style="width: 50%" align="LEFT" class="lbl"><asp:CheckBox ID="CHK_LUMBAR_NERVE_EVALUATION" runat="server" Text="Lumbarsacral Nerves Evaluation" /></td>
       </tr>
       </table>
  <table width="100%">
            
            <tr>
            <td style="width: 50%; height: 21px;" align="Center" class="lbl">
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; Reason For Ordering Test</strong></td></tr></table>
                <table width="100%">
            
            <tr>
            <td style="width: 50%; height: 21px;" align="Left" class="lbl">
                <strong>Subjective Findings</strong></td></tr></table>
                <table width="100%">
                
                
                <tr>
            <td style="width: 15%; height: 20px;" align="center" class="lbl"><asp:CheckBox ID="CHK_NECK_PAIN" runat="server" Text=" " /></td>
           
            <td style="width: 35%; height: 20px;" align="left" class="lbl">Neck Pain & Stiffness</td>
            <td style="width: 15%; height: 20px;" align="center" class="lbl"><asp:CheckBox ID="CHK_LOWER_BACK_PAIN" runat="server" Text=" " /></td>
            
            <td style="width: 35%; height: 20px;" align="left" class="lbl">Lower Back Pain & Stiffness</td>
            </tr> 
             <tr>
            <td style="width: 15%" align="center" class="lbl"><asp:CheckBox ID="CHK_UPPER_BACK_PAIN" runat="server" Text=" " /></td>
           
            <td style="width: 35%" align="left" class="lbl">Upper Back Pain & Stiffness</td>
            <td style="width: 15%" align="center" class="lbl"><asp:CheckBox ID="CHK_MIDDLE_BACK_PAIN" runat="server" Text=" " /></td>
            
            <td style="width: 35%" align="left" class="lbl">Middle Back Pain & Stiffness</td>
       </tr>
      
                </table>
                  <table width="100%">
            
            <tr>
            <td style="width: 50%; height: 21px;" align="Center" class="lbl">
                <strong>Upper Extremities</strong></td>
                <td style="width: 50%; height: 21px;" align="Center" class="lbl">
                <strong>Lower Extremities </strong></td>
                
                </tr>
                </table>
                 <table width="100%">
            
            <tr>
            <td style="width: 50%; height: 21px;" align="Center" class="lbl">(Please check off clinical/working diagnosis and laterality of suspected lesion)
              </td></tr></table>
              <table width="100%">
              <tr>
             <td align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_839_08_UPPER_MULTIPLE_SUBLUXATION" runat="server" RepeatDirection="Horizontal"
                     RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0">   </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 35%" align="left" class="lbl">839.08 Multiple Subluxation</td>
             <td align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_839_2_LOWER_LUMBER_SUBLAXATION" runat="server" RepeatDirection="Horizontal"
                     RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 32%" align="left" class="lbl">839.2 Lumbar Sublaxations</td>
           
       </tr>
       <tr>
             <td align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_723_1_UPPER_CERVICALGIA" runat="server" RepeatDirection="Horizontal"
                     RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 35%" align="left" class="lbl">723.1 Cervicalgia</td>
             <td align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_839_42_LOWER_SACROILLIAC_SUBLUXATIONS" runat="server"
                     RepeatDirection="Horizontal" RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 32%" align="left" class="lbl">839.42 Sacroilliac Subluxations</td>
           
       </tr>
          <tr>
             <td align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_723_2_UPPER_CERVICOCRANIAL_SYNDROME" runat="server"
                     RepeatDirection="Horizontal" RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 35%" align="left" class="lbl">
                 723.2 Cervicocranial Syndrome</td>
             <td align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_724_2_LOWER_LUMBOSACRAL_RADICULITIS" runat="server"
                     RepeatDirection="Horizontal" RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 32%" align="left" class="lbl">
                 724.2 Lumbosacral Radiculitis</td>
           
       </tr>
        <tr>
             <td align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_723_3_UPPER_CERVICOBRACHIAL_SYNDROME" runat="server"
                     RepeatDirection="Horizontal" RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 35%" align="left" class="lbl">
                 723.3 Cervicobrachial Syndrome</td>
             <td align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_724_8_LOWER_LUMBAGO" runat="server" RepeatDirection="Horizontal"
                     RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 32%" align="left" class="lbl">
                 724.8 Lumbago</td>
           
       </tr>
       
        <tr>
             <td style="height: 20px;" align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_723_4_UPPER_CERVICAL_REDICULITIS" runat="server" RepeatDirection="Horizontal"
                     RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 35%; height: 20px;" align="left" class="lbl">
                 723.4 Cervical Rediculitis</td>
             <td style="height: 20px;" align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_723_4_LOWER_LUMBAR_FACET_SYNDROME" runat="server" RepeatDirection="Horizontal"
                     RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 32%; height: 20px;" align="left" class="lbl">
                 723.4 Lumbar Facet Syndrome</td>
           
       </tr>
        <tr>
             <td align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_729_1_UPPER_MYALGIA_MYOSITIS" runat="server" RepeatDirection="Horizontal"
                     RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 35%" align="left" class="lbl">
                 729.1 Myalgia/Myositis</td>
             <td align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_729_1_LOWER_MYALGIA_MYOSITIS" runat="server" RepeatDirection="Horizontal"
                     RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 32%" align="left" class="lbl">
                 729.1 Myalgia/Myositis</td>
           
       </tr>
         <tr>
             <td align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_728_85_UPPER_MUSCLE_SPASM" runat="server" RepeatDirection="Horizontal"
                     RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 35%" align="left" class="lbl">
                 728.85 Muscls Spasm</td>
             <td align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_728_85_LOWER_MUSCLE_SPASM" runat="server" RepeatDirection="Horizontal"
                     RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 32%" align="left" class="lbl">
                 728.85 Muscls Spasm</td>
           
       </tr>
       <tr>
             <td align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_782_0_UPPER_NUMBNESS_TINGLING" runat="server" RepeatDirection="Horizontal"
                     RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 35%" align="left" class="lbl">
                 782.0 Numbness/Tingling</td>
             <td align="center" colspan="3" class="lbl">
                 <asp:RadioButtonList ID="RDO_782_0_LOWER_NUMBNESS_TINGLING" runat="server" RepeatDirection="Horizontal"
                     RepeatLayout="Flow" Width="92px">
                     <asp:ListItem Value="0"> </asp:ListItem>
                     <asp:ListItem Value="1"> </asp:ListItem>
                     <asp:ListItem Value="2"> </asp:ListItem>
                 </asp:RadioButtonList></td>
             <td style="width: 32%" align="left" class="lbl">
                 782.0 Numbness/Tingling</td>
           
       </tr>
      
       </table>
       <table width="100%">
      
        <tr>
            <td style="width: 20%; height: 26px;" align="LEFT" class="lbl">
                &nbsp; Other:</td>
            <td style="width: 80%; height: 26px;" align="left"> <asp:TextBox ID="TXT_OTHER" runat="server" Width="616px" CssClass="textboxCSS"></asp:TextBox></td>
       </tr>
        </table>
        <table width="100%">
            
            <tr>
            <td style="width: 50%; height: 21px;" align="LEFT" class="lbl">
                <strong>Additional Clinical Information</strong></td></tr></table>
                <table width="100%">
                
                
                <tr>
            <td style="width: 100%" align="left" class="lbl"><asp:CheckBox ID="CHK_SPINAL" runat="server" Text="Rule in/out spinal nerve root lesion(Spinal Level,Laterality,Severity)" /></td>
            </tr>
            <tr>
            <td style="width: 100%" align="left" class="lbl"><asp:CheckBox ID="CHK_SUSPECTED" runat="server" Text="Rule in/out suspected preipheral nerve entrapment syndrome" /></td>
            </tr>
            <tr>
            <td style="width: 100%" align="left" class="lbl"><asp:CheckBox ID="CHK_REFERRED" runat="server" Text="Rule in/out referred pain syndrome(Myofacial or sclerotogenous origin vs. nerve root lesion)" /></td>
            </tr>
            
            
            
            
            
            </table>
            <table width="100%">
            <tr>
            <td style="width: 50%" align="LEFT" class="lbl">
                &nbsp;<strong > Doctor's Comments:</strong></td></tr>
            <tr><td style="width: 50%;" align="left"> 
                <asp:TextBox ID="TXT_DOCTOR_COMMENTS" runat="server" TextMode="MultiLine" Width="90%" CssClass="textboxCSS"></asp:TextBox></td>
       </tr>
        </table>
        <table width="100%">
       
                  <tr>
                   
                <td align="Center" style="width: 50%"> <asp:Button ID="BtnSave" runat="server" Text="Save&Next" OnClick="BtnSave_Click" Width="87px" CssClass="Buttons" /></td>
            </tr>
            <tr>
                <td align="center" class="lbl" style="width: 50%">
                    <asp:TextBox ID="TXT_I_EVENT" runat="server" Visible="False"
                    Width="12px"></asp:TextBox>
                    <asp:TextBox ID="TXT_839_08_UPPER_MULTIPLE_SUBLUXATION" runat="server" Width="14px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TXT_723_1_UPPER_CERVICALGIA" runat="server" Width="9px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TXT_723_2_UPPER_CERVICOCRANIAL_SYNDROME" runat="server" Width="10px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TXT_723_3_UPPER_CERVICOBRACHIAL_SYNDROME" runat="server" Width="11px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TXT_723_4_UPPER_CERVICAL_REDICULITIS" runat="server" Width="5px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TXT_729_1_UPPER_MYALGIA_MYOSITIS" runat="server" Width="11px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TXT_728_85_UPPER_MUSCLE_SPASM" runat="server" Width="3px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TXT_782_0_UPPER_NUMBNESS_TINGLING" runat="server" Width="13px" Visible="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="center" class="lbl" style="width: 50%">
                    <asp:TextBox ID="TXT_839_2_LOWER_LUMBER_SUBLAXATION" runat="server" Width="14px" Visible="False"></asp:TextBox><asp:TextBox
                        ID="TXT_839_42_LOWER_SACROILLIAC_SUBLUXATIONS" runat="server" Width="9px" Visible="False"></asp:TextBox><asp:TextBox
                            ID="TXT_724_2_LOWER_LUMBOSACRAL_RADICULITIS" runat="server" Width="10px" Visible="False"></asp:TextBox><asp:TextBox
                                ID="TXT_724_8_LOWER_LUMBAGO" runat="server" Width="11px" Visible="False"></asp:TextBox><asp:TextBox
                                    ID="TXT_723_4_LOWER_LUMBAR_FACET_SYNDROME" runat="server" Width="5px" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_729_1_LOWER_MYALGIA_MYOSITIS"
                                        runat="server" Width="11px" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_728_85_LOWER_MUSCLE_SPASM" runat="server"
                                            Width="3px" Visible="False"></asp:TextBox><asp:TextBox ID="TXT_782_0_LOWER_NUMBNESS_TINGLING" runat="server" Width="13px" Visible="False"></asp:TextBox></td>
            </tr>
       
       
       </table>
    
                
   </td>
         </tr>
     </table>
       </asp:Content>
    