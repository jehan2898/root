<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_ECG_Form2.aspx.cs" Inherits="Bill_Sys_ECG_Form2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <table width="100%">
 <tr>
 <td width="100%"  class="TDPart">
    
    <table width="100%">
    <tr><td style="width: 24%; height: 26px;" class="lbl">Name(Nombre):</td><td style="height: 26px" class="lbl"><asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="70%" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td><td colspan="2" style="width: 25%; height: 26px;" class="lbl">
        Age(Edad):</td><td style="height: 26px" class="lbl"><asp:TextBox ID="TXT_AGE" runat="server" Width="35%" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
    </tr>
    </table>
    <table width="100%">
     <tr><td colspan="2" style="width: 24%" align="left" class="lbl">D.O.A(Fecha De Accidente)</td><td class="lbl"><asp:TextBox ID="TXT_DOA_FECHA_DE" runat="server" Width="175px" CssClass="textboxCSS"></asp:TextBox></td></tr>
    
    </table>
    <table width="100%">
     <tr><td colspan="2" style="width: 31%; height: 30px;" class="lbl">Were You The(Usted Era):</td>
     <td align="left" colspan="4" style="height: 30px" class="lbl">
         <asp:RadioButtonList ID="RDO_USTED_ERA" runat="server" RepeatDirection="Horizontal"
             RepeatLayout="Flow" Width="488px" CssClass="lbl">
             <asp:ListItem Value="0">Driver(Choper)</asp:ListItem>
             <asp:ListItem Value="1">Passenger(Pasajero)</asp:ListItem>
             <asp:ListItem Value="2">Pedestrian(Peaton)</asp:ListItem>
             <asp:ListItem Value="3">Bicyclist</asp:ListItem>
         </asp:RadioButtonList></td>
     </tr>
    
    </table>
    <table width="100%">
       
                  <tr>
                   
                <td align="center" style="width: 50%; height: 22px;"> 
                    <asp:Button ID="Button1" runat="server" CssClass="Buttons" OnClick="Button1_Click"
                        Text="Previous" Width="77px" />
                    <asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click" Width="75px" CssClass="Buttons" /></td>
            </tr>
        <tr>
            <td align="center" class="lbl" style="width: 50%">
                <asp:TextBox ID="TXT_I_EVENT" runat="server" Width="22px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="TXT_USTED_ERA" runat="server" Width="20px" Visible="False"></asp:TextBox></td>
        </tr>
       
       
       </table>
    
    
    
   </td>
         </tr>
     </table>
       </asp:Content>
