<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CO_Chiro_Ca.aspx.cs" Inherits="Bill_Sys_CO_Chiro_Ca"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table class="ContentTable" width="100%">
        <tr>
            <td class="TDPart" style="height: 62px">
                <table width="100%">
                    <tr>
                        <td align="center" style="height: 18px; width: 93px;">
                            <asp:Label ID="lblHeading" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 93px">
                            Patient's Name
                        </td>
                        <td style="width: 46%">
                            <asp:TextBox ID="txtPatientName" runat="server" Width="69%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="lbl" style="width: 13%">
                            Date of accident
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="txtDOA" runat="server" Width="63%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                            <td style="width: 93px">
                            <asp:Label ID="Lbl_date_Cap" runat="server" Text="Date" CssClass="lbl"></asp:Label>                            
                            </td>
                            <td>
                                <asp:Label ID="LBL_DATE" runat="server" Width="48px" CssClass="lbl"></asp:Label>
                            </td>
                        </tr>
                        
                </table>
            
         </tr>
         <tr>
         <td class="TDPart">
         
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 841px">
                             
                                <table style="width: 100%">
                                    <tr style="width:100%">
                                        <td colspan="3" align="left" style="height: 18px">
                                            <asp:Label ID="lbl_treatment" runat="server" Font-Bold="True" Text="Treatment" CssClass="class=LeftTop"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="width:100%">
                                        <td style="height: 20px">
                                            <asp:CheckBox ID="CHK_TREATNENT_CA" runat="server" Text="Cervical Adjustment" Width="148px"></asp:CheckBox>
                                        </td>
                                        <td style="height: 20px">
                                            <asp:CheckBox ID="CHK_TREATNENT_DA" runat="server" Text="Dorsal Adjustment" Width="139px"></asp:CheckBox>
                                        </td>
                                        <td style="height: 20px">
                                            <asp:CheckBox ID="CHK_TREATNENT_LA" runat="server" Text="Limber Adjust" Width="109px"></asp:CheckBox>
                                        </td>
                                    
                                        <td style="height: 20px">
                                            <asp:CheckBox ID="CHK_TREATNENT_EA" runat="server" Text="Extremity Adjustment" Width="154px"></asp:CheckBox>
                                        </td>
                                        <td colspan="2" align="left" style="height: 20px">
                                            <asp:CheckBox ID="CHK_TREATNENT_FS" runat="server" Text="Full Spine Adjustment" Width="152px"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CHK_TREATNENT_NP" runat="server" Text="New PL Exam" Width="111px"></asp:CheckBox>
                                        </td>
                                        <td colspan="2" align="left">
                                            <asp:CheckBox ID="CHK_TREATNENT_RE" runat="server" Text="Re Examination" Width="124px"></asp:CheckBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr style="width:100%">
                                        <td >
                                            <asp:Label ID="lbl_neck" runat="server" Font-Bold="True" Text="Neck" CssClass="lbl"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="width:100%">
                                        <td  >
                                            <table>
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList ID="rdl_NECK" runat="server" RepeatColumns="7" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="758px" >
                                                        <asp:ListItem Value="1">Much Better</asp:ListItem>
                                                        <asp:ListItem Value="2">Doing Fair/Better</asp:ListItem>
                                                        <asp:ListItem Value="3">Little Improvement</asp:ListItem>
                                                        <asp:ListItem Value="4">ame/No Change</asp:ListItem>
                                                        <asp:ListItem Value="5">Worse</asp:ListItem>
                                                        <asp:ListItem Value="6">New Condition</asp:ListItem>
                                                        <asp:ListItem Value="7">Much Worse</asp:ListItem>
                                                        <asp:ListItem Value="8">Pt responds well to Care</asp:ListItem>
                                                        <asp:ListItem Value="9">Pt Responds slow to Care</asp:ListItem>
                                                        <asp:ListItem Value="10">Cont care 3x/wk</asp:ListItem>
                                                        <asp:ListItem Value="11">Reduce care to 2x/wk</asp:ListItem>
                                                        <asp:ListItem Value="13">See additional note</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td>
                                                <asp:TextBox ID="txt_NECK" runat="server" Text="-1" Visible="False" Width="17px"></asp:TextBox>
                                            </td>   
                                            </tr>
                                            </table>
                                        </td>
                                                                            
                                        <%--<td colspan="2" align="left">
                                            <asp:TextBox ID="TXT_SEE_ADDITIONAL_NOTE" runat="server" Width="456PX">
                                            </asp:TextBox>
                                        </td>--%>
                                    </tr>
                                </table>
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="3" align="left">
                                            <asp:Label ID="lbl_UPPER_BACK" runat="server" Font-Bold="True" Text="Upper Back" CssClass="lbl"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                        <td>                                                                          
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="rdl_UPPER_BACK" runat="server" RepeatColumns="7" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="762px">
                                                        <asp:ListItem Value="1">Much Better</asp:ListItem>
                                                        <asp:ListItem Value="2">Doing Fair/Better</asp:ListItem>
                                                        <asp:ListItem Value="3">Little Improvement</asp:ListItem>
                                                        <asp:ListItem Value="4">ame/No Change</asp:ListItem>
                                                        <asp:ListItem Value="5">Worse</asp:ListItem>
                                                        <asp:ListItem Value="6">New Condition</asp:ListItem>
                                                        <asp:ListItem Value="7">Much Worse</asp:ListItem>
                                                        <asp:ListItem Value="8">Pt responds well to Care</asp:ListItem>
                                                        <asp:ListItem Value="9">Pt Responds slow to Care</asp:ListItem>
                                                        <asp:ListItem Value="10">Cont care 3x/wk</asp:ListItem>
                                                        <asp:ListItem Value="11">Reduce care to 2x/wk</asp:ListItem>
                                                        <asp:ListItem Value="13">See additional note</asp:ListItem>                                                   
                                                    </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                 <tr>
                                            <td>
                                                <asp:TextBox ID="txt_UPPER_BACK" runat="server" Text="-1" Visible="False" Width="11px"></asp:TextBox>
                                            </td>   
                                            </tr>
                                            </table>
                                        
                                        </td>
                                    </tr>
                                    
                                    
                                   <tr>
                                  
                                        <%-- <td colspan="2" align="left">
                                            <asp:TextBox ID="TextBox1" runat="server" Width="456PX">
                                            </asp:TextBox>
                                        </td>--%>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="height: 18px">
                                            <asp:Label ID="lbl_LOW_BACK" runat="server" Font-Bold="True" Text="Low Back" CssClass="lbl"></asp:Label>
                                        </td>
                                     
                                     </tr>                                    
                                     <tr>
                                      
                                       <%-- <table style="width: 122%">
                                            <tr>
                                                <td style="width: 139px">
                                                    <asp:RadioButton ID="RDO_LOW_BACK_MUCH_BETTER" runat="server" GroupName="Low_Back"
                                                    Text="Much Better" Width="94px"></asp:RadioButton>
                                                </td>
                                                <td style="width: 159px">
                                                    <asp:RadioButton ID="RDO_LOW_BACK_DOING_FAIR_BETTER" runat="server" GroupName="Low_Back"
                                                    Text="Doing Fair/Better" Width="127px"></asp:RadioButton>
                                                </td>
                                                <td style="width: 180px">
                                                    <asp:RadioButton ID="RDO_LOW_BACK_LITTLE_IMPROVEMENT" runat="server" GroupName="Low_Back"
                                                    Text="Little Improvement" Width="131px"></asp:RadioButton>
                                                </td>                                    
                                                <td>
                                                    <asp:RadioButton ID="RDO_LOW_BACK_SAME_NOCHANGE" runat="server" GroupName="Low_Back"
                                                    Text="Same/No Change" Width="124px"></asp:RadioButton>
                                               </td>
                                                <td align="left">
                                                    <asp:RadioButton ID="RDO_LOW_BACK_WORSE" runat="server" GroupName="Low_Back" Text="Worse">
                                                    </asp:RadioButton>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 139px">
                                                    <asp:RadioButton ID="RDO_LOW_BACK_MUCH_WORSE" runat="server" GroupName="Low_Back"
                                                    Text="Much Worse" Width="101px"></asp:RadioButton>
                                                </td>
                                                  <td style="width: 159px">
                                                        <asp:RadioButton ID="RDO_LOW_BACK_NEW_CONDITION" runat="server" GroupName="Low_Back"
                                                       Text="New Condition" Width="119px"></asp:RadioButton>
                                                 </td>
                                                  <td style="width: 180px">
                                                    <asp:RadioButton ID="RDO_LOW_BACK_PT_RESPONDS_WELL_TO_CARE" GroupName="Low_Back"
                                                    runat="server" Text="Pt responds well to Care" Width="196px"></asp:RadioButton>
                                                </td>
                                                  <td>
                                                    <asp:RadioButton ID="RDO_LOW_BACK_PT_RESPONDS_SLOW_TO_CARE" GroupName="Low_Back"
                                                    runat="server" Text="Pt Responds slow to Care" Width="171px"></asp:RadioButton>
                                                 </td>
                                                 
                                       
                                            </tr>
                                            <tr>
                                                                <td align="left" style="width: 139px">
                                            <asp:RadioButton ID="RadioButton2" GroupName="Low_Back" runat="server"
                                                Text="Cont care 3x/wk" Width="129px"></asp:RadioButton>
                                        </td>
                                        <td style="width: 159px">
                                            <asp:RadioButton ID="RadioButton3" GroupName="Low_Back" runat="server"
                                                Text="Reduce care to 2x/wk" Width="151px"></asp:RadioButton>
                                        </td>
                                        <td style="width: 180px">
                                            <asp:RadioButton ID="RadioButton4" GroupName="Low_Back" runat="server"
                                                Text="Reduce care to 1x/wk" Width="167px"></asp:RadioButton>
                                        </td>
                                        <td colspan="3" align="left">
                                            <asp:RadioButton ID="RDO_LOW_BACK_SEE_ADDITIONAL_NOTE" GroupName="Low_Back" runat="server"
                                                Text="See additional note" Height="18px" Width="131px"></asp:RadioButton>
                                        </td>
                                            
                                            </tr>
                                          </table>--%>
                                    </tr>
                                    
                                </table>
                                                    <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="rdl_LOW_BACK" runat="server" RepeatColumns="7" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="765px">
                                                        <asp:ListItem Value="1">Much Better</asp:ListItem>
                                                        <asp:ListItem Value="2">Doing Fair/Better</asp:ListItem>
                                                        <asp:ListItem Value="3">Little Improvement</asp:ListItem>
                                                        <asp:ListItem Value="4">ame/No Change</asp:ListItem>
                                                        <asp:ListItem Value="5">Worse</asp:ListItem>
                                                        <asp:ListItem Value="6">New Condition</asp:ListItem>
                                                        <asp:ListItem Value="7">Much Worse</asp:ListItem>
                                                        <asp:ListItem Value="8">Pt responds well to Care</asp:ListItem>
                                                        <asp:ListItem Value="9">Pt Responds slow to Care</asp:ListItem>
                                                        <asp:ListItem Value="10">Cont care 3x/wk</asp:ListItem>
                                                        <asp:ListItem Value="11">Reduce care to 2x/wk</asp:ListItem>
                                                        <asp:ListItem Value="13">See additional note</asp:ListItem>                                                   
                                                    </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                            <td style="height: 22px">
                                                <asp:TextBox ID="txt_LOW_BACK" runat="server" Text="-1" Visible="False" Width="15px"></asp:TextBox>
                                            </td>   
                                            </tr>
                                            </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                                <table width="100%">
                                    <tr>
                                        <td colspan="3" align="left">
                                            <asp:Label ID="lbl_SHOULDER" runat="server" Font-Bold="True" Text="Shoulder" CssClass="lbl"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                      <%--  <table>
                                        <tr>
                                            <td style="width: 148px">
                                                <asp:RadioButton ID="RDO_SHOULDER_MUCH_BETTER" runat="server" GroupName="SHOULDER"
                                                 Text="Much Better" Width="103px"></asp:RadioButton>
                                            </td>
                                            <td style="width: 147px">
                                                <asp:RadioButton ID="RDO_SHOULDER_DOING_FAIR_BETTER" runat="server" GroupName="SHOULDER"
                                                Text="Doing Fair/Better" Width="127px"></asp:RadioButton>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RDO_SHOULDER_LITTLE_IMPROVEMENT" runat="server" GroupName="SHOULDER"
                                                Text="Little Improvement" Width="130px"></asp:RadioButton>
                                            </td>                                    
                                            <td>
                                                <asp:RadioButton ID="RDO_SHOULDER_SAME_NOCHANGE" runat="server" GroupName="SHOULDER"
                                                Text="Same/No Change" Width="124px"></asp:RadioButton>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButton ID="RDO_SHOULDER_WORSE" runat="server" GroupName="SHOULDER" Text="Worse" Width="79px">
                                                </asp:RadioButton>
                                            </td>
                                            
                                        </tr>
                                         <tr>
                                             <td align="left" style="width: 148px">
                                                <asp:RadioButton ID="RDO_SHOULDER_MUCH_WORSE" runat="server" GroupName="SHOULDER"
                                                Text="Much Worse" Width="108px"></asp:RadioButton>
                                            </td>
                                             <td style="width: 147px">
                                              <asp:RadioButton ID="RDO_SHOULDER_NEW_CONDITION" runat="server" GroupName="SHOULDER"
                                                Text="New Condition" Width="119px"></asp:RadioButton>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButton ID="RDO_SHOULDER_PT_RESPONDS_WELL_TO_CARE" GroupName="SHOULDER"
                                                runat="server" Text="Pt responds well to Care" Width="197px"></asp:RadioButton>
                                             </td>
                                            <td>
                                                <asp:RadioButton ID="RDO_SHOULDER_PT_RESPONDS_SLOW_TO_CARE" GroupName="SHOULDER"
                                                runat="server" Text="Pt Responds slow to Care" Width="202px"></asp:RadioButton>
                                            </td>
                                            <td align="left">
                                            <asp:RadioButton ID="RDO_SHOULDER_PT_RESPONDS_SLOW_3XWK" GroupName="SHOULDER" runat="server"
                                                Text="Cont care 3x/wk" Width="129px"></asp:RadioButton>
                                        </td>
                                       
                                            
                                    </tr>
                                    <tr>
                                         <td style="width: 148px">
                                            <asp:RadioButton ID="RDO_SHOULDER_PT_RESPONDS_SLOW_2XWK" GroupName="SHOULDER" runat="server"
                                                Text="Reduce care to 2x/wk" Width="151px"></asp:RadioButton>
                                        </td>
                                        <td align="left" style="width: 147px">
                                            <asp:RadioButton ID="RDO_SHOULDER_PT_RESPONDS_SLOW_1XWK" GroupName="SHOULDER" runat="server"
                                                Text="Reduce care to 1x/wk" Width="146px"></asp:RadioButton>
                                        </td>
                                        <td colspan="3" align="left">
                                            <asp:RadioButton ID="RDO_SHOULDER_SEE_ADDITIONAL_NOTE" GroupName="SHOULDER" runat="server"
                                                Text="See additional note" Width="157px"></asp:RadioButton>
                                        </td>
                                    
                                    </tr>
                                        
                                       </table>--%>
                                   
                                                    <asp:RadioButtonList ID="rdl_SHOULER" runat="server" RepeatColumns="7" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="769px">
                                                        <asp:ListItem Value="1">Much Better</asp:ListItem>
                                                        <asp:ListItem Value="2">Doing Fair/Better</asp:ListItem>
                                                        <asp:ListItem Value="3">Little Improvement</asp:ListItem>
                                                        <asp:ListItem Value="4">ame/No Change</asp:ListItem>
                                                        <asp:ListItem Value="5">Worse</asp:ListItem>
                                                        <asp:ListItem Value="6">New Condition</asp:ListItem>
                                                        <asp:ListItem Value="7">Much Worse</asp:ListItem>
                                                        <asp:ListItem Value="8">Pt responds well to Care</asp:ListItem>
                                                        <asp:ListItem Value="9">Pt Responds slow to Care</asp:ListItem>
                                                        <asp:ListItem Value="10">Cont care 3x/wk</asp:ListItem>
                                                        <asp:ListItem Value="11">Reduce care to 2x/wk</asp:ListItem>
                                                        <asp:ListItem Value="13">See additional note</asp:ListItem>                                                   
                                                    </asp:RadioButtonList>
                                        
                                    </tr>
                                      <tr>
                                            <td>
                                                <asp:TextBox ID="txt_SHOULER" runat="server" Text="-1" Visible="False" Width="20px"></asp:TextBox>
                                            </td>   
                                            </tr>
                                 
                                      
                              
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td colspan="3" align="left">
                                            <asp:Label ID="lbl_KNEE" runat="server" Font-Bold="True" Text="Knee" CssClass="lbl"></asp:Label>
                                        </td>
                                     </tr>
                                 <%--    <tr style="width:100%">
                                    
                                        <td style="width: 159px">
                                            <asp:RadioButton ID="RDO_KNEE_MUCH_BETTER" runat="server" GroupName="KNEE" Text="Much Better" Width="117px">
                                            </asp:RadioButton>
                                        </td>
                                        <td >
                                            <asp:RadioButton ID="RDO_KNEE_DOING_FAIR_BETTER" runat="server" GroupName="KNEE"
                                                Text="Doing Fair/Better" Width="140px"></asp:RadioButton>
                                        </td>
                                        <td style="width: 169px">
                                            <asp:RadioButton ID="RDO_KNEE_LITTLE_IMPROVEMENT" runat="server" GroupName="KNEE"
                                                Text="Little Improvement" Width="148px"></asp:RadioButton>
                                        </td>
                                    
                                        <td>
                                            <asp:RadioButton ID="RDO_KNEE_SAME_NOCHANGE" runat="server" GroupName="KNEE" Text="Same/No Change" Width="134px">
                                            </asp:RadioButton>
                                        </td>
                                        <td align="left">
                                            <asp:RadioButton ID="RDO_KNEE_WORSE" runat="server" GroupName="KNEE" Text="Worse"></asp:RadioButton>
                                        </td>
                                      </tr> --%>
                                    <%--  <tr style="width:100%">
                                        
                                        <td align="left" style="width: 159px">
                                            <asp:RadioButton ID="RDO_KNEE_MUCH_WORSE" runat="server" GroupName="KNEE" Text="Much Worse" Width="110px">
                                            </asp:RadioButton>
                                        </td>
                                    
                                        <td>
                                            <asp:RadioButton ID="RDO_KNEE_NEW_CONDITION" runat="server" GroupName="KNEE" Text="New Condition">
                                            </asp:RadioButton>
                                        </td>
                                        <td align="left" style="width: 169px">
                                            <asp:RadioButton ID="RDO_KNEE_PT_RESPONDS_WELL_TO_CARE" GroupName="KNEE" runat="server"
                                                Text="Pt responds well to Care" Width="182px"></asp:RadioButton>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="RDO_KNEE_PT_RESPONDS_SLOW_TO_CARE" GroupName="KNEE" runat="server"
                                                Text="Pt Responds slow to Care" Width="176px"></asp:RadioButton>
                                        </td>
                                                                            <td align="left">
                                            <asp:RadioButton ID="RDO_KNEE_PT_RESPONDS_SLOW_3XWK" GroupName="KNEE" runat="server"
                                                Text="Cont care 3x/wk" Width="141px"></asp:RadioButton>
                                        </td>
                                       </tr>--%>
                                       <%--<tr style="width:100%">
                                        <td style="width: 159px; height: 20px;">
                                            <asp:RadioButton ID="RDO_KNEE_PT_RESPONDS_SLOW_2XWK" GroupName="KNEE" runat="server"
                                                Text="Reduce care to 2x/wk" Width="153px"></asp:RadioButton>
                                        </td>
                                        <td align="left" style="height: 20px">
                                            <asp:RadioButton ID="RDO_KNEE_PT_RESPONDS_SLOW_1XWK" GroupName="KNEE" runat="server"
                                                Text="Reduce care to 1x/wk" Width="157px"></asp:RadioButton>
                                        </td>
                                        <td colspan="3" align="left" style="height: 20px">
                                            <asp:RadioButton ID="RDO_KNEE_SEE_ADDITIONAL_NOTE" GroupName="KNEE" runat="server"
                                                Text="See additional note" Width="150px"></asp:RadioButton>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rdl_KNEE" runat="server" RepeatColumns="7" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="767px">
                                                 <asp:ListItem Value="1">Much Better</asp:ListItem>
                                                 <asp:ListItem Value="2">Doing Fair/Better</asp:ListItem>
                                                 <asp:ListItem Value="3">Little Improvement</asp:ListItem>
                                                 <asp:ListItem Value="4">ame/No Change</asp:ListItem>
                                                 <asp:ListItem Value="5">Worse</asp:ListItem>
                                                 <asp:ListItem Value="6">New Condition</asp:ListItem>
                                                 <asp:ListItem Value="7">Much Worse</asp:ListItem>
                                                 <asp:ListItem Value="8">Pt responds well to Care</asp:ListItem>
                                                 <asp:ListItem Value="9">Pt Responds slow to Care</asp:ListItem>
                                                 <asp:ListItem Value="10">Cont care 3x/wk</asp:ListItem>
                                                 <asp:ListItem Value="11">Reduce care to 2x/wk</asp:ListItem>
                                                 <asp:ListItem Value="13">See additional note</asp:ListItem>                                                   
                                              </asp:RadioButtonList>
                                           </td>
                                       </tr>
                                       <tr>
                                            <td>
                                                <asp:TextBox ID="txt_KNEE" runat="server" Text="-1" Visible="False" Width="17px"></asp:TextBox>
                                            </td>   
                                            </tr>
                           
                                </table>
                                 <table style="width: 100%">
                                    <tr>
                                        <td align="center">
                                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                                            <asp:Button ID="css_btnSave" runat="server" Text="Save" OnClick="css_btnSave_Click"
                                                CssClass="Buttons" />
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                                                CssClass="Buttons" Visible="False" />
                                            <asp:TextBox ID="txtEventID" runat="Server" Visible="false" Width="10px"></asp:TextBox>
                                            <asp:TextBox ID="txtCaseID" runat="Server" Visible="false" Width="10px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
            </td>
        </tr>
    </table>
</asp:Content>
