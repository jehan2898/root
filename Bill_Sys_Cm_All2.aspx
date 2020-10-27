<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_Cm_All2.aspx.cs" Inherits="Bill_Sys_Cm_All2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <table width="100%">
 <tr>
 <td width="100%"  class="TDPart">
    <table width="100%">
     <tr>
       
       <td style="width: 20%" align="LEFT" class="lbl">NAME:</td>
           <td style="width: 20%" align="left" class="lbl"></td> <td align="left" colspan="3"> <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="530px" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
       </tr>
       </table>
       
           <table width="100%">
        <tr>
            <td style="width: 19%" align="LEFT" class="lbl">DATE OF ACCIDENT</td>
            <td style="width: 35%" align="center" class="lbl"> <asp:TextBox ID="TXT_DOA" runat="server" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
            
       </tr></table>
        <table width="100%">
       <tr>
            <td style="width: 50%" align="LEFT" class="lbl">1.AT THE TIME OF ACCIDENT I WAS THE</td>
            <td style="width: 50%" align="center" class="lbl"> <asp:TextBox ID="TXT_PQ_AT_THE_TIME_OF_ACCIDENT_I_WAS" runat="server" Width="339px" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
             </table>
             <table width="100%">
       <tr>
            <td style="width: 50%; height: 22px;" align="LEFT" class="lbl">2.WERE YOU INJURED?</td>
            <td style="width: 50%; height: 22px;" align="left" class="lbl"> <asp:TextBox ID="TXT_PQ_WERE_U_INJURED" runat="server" Width="336px" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
             </table>
              <table width="100%">
       <tr>
            <td style="width: 100%" align="LEFT" class="lbl">3.LIST THE DATES OF ALL PRIOR AUTOMOBILE ACCIDENTS THAT YOU WERE INVOLVED IN FOR THE &nbsp; LAST THREE YEARS AND CIRCUMSTANCES SURROUNDING EACH OH THE ACCIDENTS.</td></tr>
            <tr>
             <td style="width: 50%; height: 34px;" align="left" class="lbl"> <asp:TextBox ID="TXT_PQ_LIST" runat="server" Width="737px" Height="32px" CssClass="textboxCSS"></asp:TextBox></td>
             </tr>
            
            </table>
              <table width="100%">
       <tr>
            <td style="width: 100%" align="LEFT" class="lbl">4.WHAT IS YOUR RELATIONSHIP TO THE OTHER PASSANGERS IN THE VEHICLE THAT YOU WERE IN?</td></tr>
            <tr>
             <td style="width: 50%; height: 29px;" align="left" class="lbl"> <asp:TextBox ID="TXT_PQ_RELN_WID_PASSANGERS" runat="server" Width="737px" Height="33px" CssClass="textboxCSS"></asp:TextBox></td>
             </tr>
            
            </table>
             <table width="100%">
       <tr>
            <td style="width: 60%" align="LEFT" class="lbl">5.DID YOU KNOW ANYONE FROM THE OTHER VEHICLE(S) INVOLVED IN THE ACCIDENT?</td>
            <td style="width: 40%" align="left"> <asp:TextBox ID="TXT_PQ_NEONE_FRM_OTHER_VEHICLE" runat="server" Width="336px" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
             </table>
               <table width="100%">
       <tr>
            <td style="width: 60%" align="LEFT" class="lbl">IF YES, PLEASE SPECIFY WHOM</td>
            <td style="width: 40%" align="left"> <asp:TextBox ID="TXT_PQ_IF_YES_SPECIYFY_WHOM" runat="server" Width="336px" CssClass="textboxCSS"></asp:TextBox></td>
            </tr>
            <tr>
            <td style="width: 60%" align="LEFT" class="lbl">6.WAS THIS ACCIDENT STAGED?</td>
            
            </tr></table>
            <table width="100%">
            
             <tr>
             
            <td style="width: 100%; height: 40px;" align="LEFT" class="lbl">PLEASE BE ADVISE THAT NEW YORK STATE LAW PROHIBITS THE FILING OF A FRAUDULENT CLAIM, WHICH IS A CRIME PUNISHABLE BY IMPRISONMENT.</td></tr>
             </table>
             <table width="100%">
        <tr>
            <td style="width: 15%" align="LEFT" class="lbl">PRINT NAME</td>
            <td style="width: 35%" align="left"> <asp:TextBox ID="TXT_PQ_PRINT_NAME" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            
      
            <td style="width: 15%" align="LEFT" class="lbl">SS#</td>
            <td style="width: 35%" align="left"> <asp:TextBox ID="TXT_PQ_SS" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            
       </tr>
       <tr>
       <td style="width: 15%" align="LEFT" class="lbl">DATE</td>
            <td style="width: 35%" align="left"> <asp:TextBox ID="TXT_PQ_DATE" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            <td><asp:TextBox ID="TXT_I_EVENT" runat="server" Width="22px" Visible="FALSE"></asp:TextBox></td>
       </tr></table>
        <table width="100%">
       
        <tr>
              
                   
               
                  
                   
                <td align="Center" style="width: 50%"> 
                    <asp:Button ID="Button1" runat="server" CssClass="Buttons" Text="Previous" OnClick="Button1_Click" />&nbsp;
                    <asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click" Width="83px" CssClass="Buttons" /></td>
            </tr>
       
       </table>
             
    
     </td>
         </tr>
     </table>
 </asp:Content>
