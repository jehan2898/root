<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_Cm_All1.aspx.cs" Inherits="Bill_Sys_Cm_All1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <table width="100%">
 <tr>
 <td width="100%"  class="TDPart">
    <table width="100%">
    
    <tr>
            <td style="width: 15%" align="left" class="lbl">PATIENT'S NAME</td>
            <td style="width: 35%" align="left" class="lbl"> &nbsp; &nbsp;
                <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="254px" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
            <td style="width: 15%" align="left" class="lbl">MAILING ADDRESS</td>
            <td style="width: 35%" align="left"> <asp:TextBox ID="TXT_MAILING_ADDR" runat="server" Width="254px"></asp:TextBox></td>
           
            
       </tr></table>
       <table>
       
       <tr>
            <td style="width: 20%" align="left" class="lbl">CITY</td>
            <td style="width: 10%" align="left"> <asp:TextBox ID="TXT_CITY" runat="server" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
            <td style="width: 15%" align="left" class="lbl">STATE</td>
           <td style="width: 10%" align="left"> <asp:TextBox ID="TXT_STATE" runat="server" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
            <td style="width: 20%" align="left" class="lbl">ZIP</td>
            <td style="width: 10%" align="left"> <asp:TextBox ID="TXT_ZIP" runat="server" Width="137px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
       </tr>
       
         <tr>
            <td style="width: 20%" align="left" class="lbl">DOB</td>
            <td style="width: 10%" align="left"> <asp:TextBox ID="TXT_DOB" runat="server" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
            <td style="width: 15%" align="left" class="lbl">SEX</td>
           <td style="width: 10%" align="left"> <asp:TextBox ID="TXT_SEX" runat="server" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
            <td style="width: 20%" align="left" class="lbl">MARRITAL STATUS</td>
            <td style="width: 10%" align="left"> <asp:TextBox ID="TXT_MARRITAL_STATUS" runat="server" Width="136px"></asp:TextBox></td>
       </tr>
       
        <tr>
            <td style="width: 20%" align="left" class="lbl">SS#</td>
            <td style="width: 10%" align="left"> <asp:TextBox ID="TXT_SS" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            <td style="width: 15%" align="left" class="lbl">HOME TEL#</td>
           <td style="width: 10%" align="left"> <asp:TextBox ID="TXT_HOME_TEL" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            <td style="width: 20%" align="left" class="lbl">ACCIDENT TYPE</td>
            <td style="width: 10%" align="left"> <asp:TextBox ID="TXT_ACCIDENT_TYPE" runat="server" Width="136px"></asp:TextBox></td>
       </tr>
       
        <tr>
            <td style="width: 20%" align="left" class="lbl">AUTO WORK OTHER</td>
            <td style="width: 10%" align="left"> <asp:TextBox ID="TXT_AUTO_WORK_OTHR" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            <td style="width: 15%" align="left" class="lbl">CELL#</td>
           <td style="width: 10%" align="left"> <asp:TextBox ID="TXT_CELL" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            <td style="width: 20%" align="left" class="lbl">DOA</td>
            <td style="width: 10%" align="left"> <asp:TextBox ID="TXT_DOA" runat="server" Width="145px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
       </tr>
       
       <tr>
            <td style="width: 10%; height: 26px;" align="left" class="lbl">PLACE OF ACCIDENT</td>
            <td style="width: 20%; height: 26px;" align="left"> <asp:TextBox ID="TXT_PLACE_ACCIDENT" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            </tr></table>
            <table>
            
            <tr>
            <td style="width: 50%; height: 21px;" align="left" class="lbl">DESCRIBE YOUR INJURIES</td></tr></table>
            <table style="width: 812px">
            
             <tr>
            <td style="width: 10%" align="left"><asp:CheckBox ID="CHK_INJ_HEADACHE" runat="server" Text="HEADACHES" CssClass="lbl" /></td>
            <td style="width: 10%" align="left"><asp:CheckBox ID="CHK_INJ_NECK" runat="server" Text="NECK" CssClass="lbl" /></td>
            <td style="width: 10%" align="left"><asp:CheckBox ID="CHK_INJ_BACK" runat="server" Text="BACK" CssClass="lbl" /></td>
            <td style="width: 10%" align="left"><asp:CheckBox ID="CHK_INJ_OTHER" runat="server" Text="OTHER" CssClass="lbl" Width="64px" /></td>
            <td style="width: 55%" align="left"> <asp:TextBox ID="TXT_INJ_OTHER" runat="server" Width="95%" CssClass="textboxCSS"></asp:TextBox></td>
            
            
       </tr>
       </table>
       <table style="width: 809px">
       
       
       <tr>
            <td style="width: 10%; height: 10px;" align="left" class="lbl"><asp:CheckBox ID="CHK_DRIVER" runat="server" Text="DRIVER" CssClass="lbl" Width="72px" /></td>
            <td style="width: 10%; height: 10px;" align="left" class="lbl"><asp:CheckBox ID="CHK_PASSANGER" runat="server" Text="PASSANGER" CssClass="lbl" Width="97px" /></td>
            <td style="width: 10%; height: 10px;" align="left" class="lbl"><asp:CheckBox ID="CHK_PEDSTRIAN" runat="server" Text="PEDSTRIAN" CssClass="lbl" Width="99px" /></td>
            <td style="width: 10%; height: 10px;" align="left" class="lbl"><asp:CheckBox ID="CHK_OTHER" runat="server" Text="OTHER" CssClass="lbl" Width="69px" /></td></tr></table>
            <table>
            <tr>
            <td style="width: 40%" align="left" class="lbl">FOR FEMALE PATIENTS ONLY:</td>
            <td style="width: 30%" align="left" class="lbl">ARE YOU PREGNANT?</td>
             <td align="left" colspan="2" class="lbl">
                 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                 <asp:RadioButtonList ID="RDO_ARE_YOU_PREGNANT" runat="server" RepeatDirection="Horizontal"
                     RepeatLayout="Flow" Width="94px">
                     <asp:ListItem Value="0">Yes</asp:ListItem>
                     <asp:ListItem Value="1">No</asp:ListItem>
                 </asp:RadioButtonList></td>
            
           
            
            </tr></table>
           <table>
            <tr>
            
            <td style="width: 1007px"></td>
            
            </tr>
            
            
      
       
      
       <tr>
            <td style="width: 1007px" align="left" class="lbl">PATIENT'S EMPLOYER</td>
            <td style="width: 35%" align="center" class="lbl"> <asp:TextBox ID="TXT_PT_EMPLOYER" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            <td style="width: 12%" align="left" class="lbl">PHONE #</td>
            <td style="width: 16%" align="center" class="lbl"> <asp:TextBox ID="TXT_PHONE" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
           
            
       </tr>
       <tr>
      
            <td style="width: 1007px" align="left" class="lbl">OCCUPATION</td>
            <td style="width: 30%" align="center" class="lbl"> <asp:TextBox ID="TXT_OCCUPATION" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            <td align="left" colspan="2" class="lbl">
                <asp:RadioButtonList ID="RDO_FULL_PART" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow" Width="108px">
                    <asp:ListItem Value="0">Full /</asp:ListItem>
                    <asp:ListItem Value="1">Part</asp:ListItem>
                </asp:RadioButtonList></td>
            <td style="width: 10%" align="left" class="lbl">DRIVER LIC #</td>
            <td style="width: 28%" align="center" class="lbl"> <asp:TextBox ID="TXT_DRIVER_LIC" runat="server"></asp:TextBox></td>
       
       
       </tr>
       <tr>
       
       <td style="width: 1007px" align="left" class="lbl">PT.EMPLOYER'S ADDRESS</td>
            <td style="width: 30%" align="center" class="lbl"> <asp:TextBox ID="TXT_EMP_ADDR" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
       
       </tr>
       <tr>
            <td style="width: 1007px" align="left" class="lbl">EMERGENCY TEL #</td>
            <td style="width: 35%" align="center" class="lbl"> <asp:TextBox ID="TXT_EMERG_TEL" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            <td style="width: 12%" align="center" class="lbl">NAME</td>
            <td style="width: 16%" align="center" class="lbl"> <asp:TextBox ID="TXT_NAME" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            
       </tr></table>
       <table>
       <tr>
       
        <td style="width: 76%" align="left" class="lbl">Is there a Law suite pending on your accident of injury?</td>
             <td style="width:102px" align="left" colspan="2">
                 <asp:RadioButtonList ID="RDO_LAWSUIT_PENDING" runat="server" RepeatDirection="Horizontal"
                     RepeatLayout="Flow" Width="105px">
                     <asp:ListItem Value="0">Yes</asp:ListItem>
                     <asp:ListItem Value="1">No</asp:ListItem>
                 </asp:RadioButtonList></td>
       
       </tr></table>
       <table style="width: 805px">
         <tr>
            
            <td></td>
            
            </tr>
              <tr>
       
       <td style="width: 50%" align="left" class="lbl">NAME OF ATTORNEY:</td>
           <td style="width: 33%" align="left" class="lbl">LAST NAME</td> 
                  <td align="left" class="lbl" colspan="2">
                      <asp:TextBox ID="TXT_NAME_OF_ATTORNEY_LAST" runat="server" CssClass="textboxCSS" Width="80%"></asp:TextBox></td>
                  <td style="width: 30%" align="center"> </td>
       </tr>
       <tr>
       
            <td style="width: 15%" align="left" class="lbl">ATTORNEY PHONE#</td>
            <td style="width: 33%" align="center"> <asp:TextBox ID="TXT_ATTORNEY_PH" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
       
       </tr>
            
            <tr>
            <td style="width: 15%" align="left" class="lbl">HEALTH INSURANCE PLAN</td>
            <td style="width: 33%" align="center"> <asp:TextBox ID="TXT_HEALTH_INSURANCE_PLAN" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            <td style="width: 15%" align="left" class="lbl">PHONE</td>
            <td style="width: 35%" align="center"> <asp:TextBox ID="TXT_HEALTH_INSURANCE_PLAN_PHONE" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
           
            
       </tr>
        <tr>
            <td style="width: 10%" align="left" class="lbl">AUTO INSURANCE</td>
            <td style="width: 33%" align="center"> <asp:TextBox ID="TXT_AUTO_INSURANCE" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            <td style="width: 15%" align="left" class="lbl">PHONE</td>
            <td style="width: 35%" align="center"> <asp:TextBox ID="TXT_AUTO_INSURANCE_PHONE" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
           
            
       </tr>
        <tr>
            <td style="width: 15%; height: 22px;" align="left" class="lbl">WORKING COMP.INS</td>
            <td style="width: 33%; height: 22px;" align="center"> <asp:TextBox ID="TXT_WORKING_COMP_INS" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
            <td style="width: 15%; height: 22px;" align="left" class="lbl">PHONE</td>
            <td style="width: 35%; height: 22px;" align="center"> <asp:TextBox ID="TXT_WORKING_COMP_INS_PHONE" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
           
            
       </tr>
        <tr>
       
        <td style="width: 70%" align="left" class="lbl">WHO REFERRED YOU TO OUR OFFICE?</td>
             <td style="width:33%" align="left"><asp:CheckBox ID="CHK_REFERRENCE_ATTORNEY" runat="server" Text="ATTORNEY" CssClass="lbl" /></td>
            <td style="width: 10%" align="left" class="lbl"><asp:CheckBox ID="CHK_REFERRENCE_FRIEND" runat="server" Text="FRIEND" CssClass="lbl" /></td>
              <td style="width: 10%" align="left" class="lbl"><asp:CheckBox ID="CHK_REFERRENCE_OTHER" runat="server" Text="OTHER" CssClass="lbl" /></td>
       
       </tr>
        <tr>
            
            <td></td>
            
            </tr>
            </table>
            <table>
               <tr>
       
        <td style="width: 100%" align="center" class="lbl">I, THE UNDERSIGNED, CERTIFY THAT THE FOLLOWING IS TRUE AND CORRECT</td> </tr></table>
        <table style="width: 803px">
         <tr>
            <td style="width: 3%; height: 13px;" align="left" class="lbl">DATE</td>
            <td style="width: 35%; height: 13px;" align="left"> <asp:TextBox ID="TXT_DATE" runat="server" CssClass="textboxCSS"></asp:TextBox>
                   
               
                  <asp:TextBox ID="TXT_I_EVENT" runat="server" Width="54px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="TXT_ARE_YOU_PREGNANT" runat="server" Width="24px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="TXT_FULL_PART" runat="server" Width="18px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="TXT_LAWSUIT_PENDING" runat="server" Width="15px" Visible="False"></asp:TextBox></td>
            

            
       </tr></table>
       <table style="width: 98%">
       
                  <tr>
                   
                <td align="center" style="width: 50%"> <asp:Button ID="BtnSave" runat="server" Text="Save&Next" OnClick="BtnSave_Click" Width="87px" CssClass="Buttons" /></td>
            </tr>
       
       
       </table>
     </td>
         </tr>
     </table>
      </asp:Content>
    