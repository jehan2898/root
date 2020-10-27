<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_MRI_QUESTIONAIRE.aspx.cs" Inherits="Bill_Sys_MRI_QUESTIONAIRE" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0" class="ContentTable" style="vertical-align:top;width:100%" align="center">
  <tr>
    <td class="TDPart">
      <table cellpadding="0" cellspacing="0" width="100%" border="0" align="center" class="ContentTable">
        <%--<tr>
          <td align="center" colspan="2" >
            <asp:TextBox ID="TXT_COMPANY_NAME" runat="server" width="500px" Height="40px"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td align="center" colspan="2" >
            <asp:TextBox ID="TXT_PREEXAM_QUESTIONARY" runat="server" Width="400px" Height="30px"></asp:TextBox>
          </td>
        </tr>--%>
        <tr>
          <td align="center"><asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label></td>
        </tr>
        <tr>
          <td align="center" width="100%"><asp:Label ID="lblCompName" runat="server" Font-Bold="True" Font-Size="X-Large" Visible="False"></asp:Label></td>
        </tr>
        <tr style="height:50px">
          <td align="left" colspan="2" >
             <asp:Label ID="Label1" runat="server" Text="MRI-QUESTIONARY" style="font-weight:bold;"></asp:Label>
             <asp:TextBox ID="TXT_EVENT_ID" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td colspan="2"  align="center">
              <br />
            <table width="100%" border="0">
              <tr>
                <td><asp:Label ID="lbl_Patient_Name" Text="Name: " runat="server"></asp:Label> <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                <td><asp:Label ID="lbl_Date_Of_Birth" runat="server" Text="D.O.B: "></asp:Label><asp:TextBox ID="TXT_DATE_OF_BIRTH" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                <td><asp:Label ID="lbl_Sex" runat="server" Text="Sex: "></asp:Label><asp:TextBox ID="TXT_SEX" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
              </tr>
            </table>
          </td> 
        </tr>
        <tr style="height:50px">
          <td align="left" colspan="2" ><asp:Label ID="lbl_header" runat="server" Text="DO YOU HAVE ANY OF THE FOLLOWING IN YOUR BODY" style="font-weight:bold;"></asp:Label></td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques1" runat="server" Text="Pacemaker, wires or defibrillator? PLEASE SEE RECEPTIONIST"></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES1" runat="server" Visible="false"></asp:TextBox>
                
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques2" runat="server" Text="Brain or aneurysm clip?"></asp:Label></td>
          <td width="40%" >
          
                 <asp:RadioButtonList ID="RDO_QUES2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
          
                 <asp:TextBox ID="TXT_QUES2" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques3" runat="server" Text="Ear or eye implants."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES3" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="0">YES</asp:ListItem>
                    <asp:ListItem Value="1">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES3" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques4" runat="server" Text="Electrical stimulators or infusion pumps."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES4" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES4" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques5" runat="server" Text="Metal fragments, shrapnel, bullets, or pellets."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES5" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES5" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques6" runat="server" Text="Magnetic implants of any kind."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES6" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES6" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques7" runat="server" Text="Coil, filter or wire in the blood vessels."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES7" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="0">YES</asp:ListItem>
                    <asp:ListItem Value="1">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES7" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques8" runat="server" Text="Artificial limb or joint."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES8" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES8" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques9" runat="server" Text="False teeth (removable). Retainers or braces."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES9" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES9" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques10" runat="server" Text="Diaphragm or intrauterine devices."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES10" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques11" runat="server" Text="Orthopedichardware of any kind (screws or plates)."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES11" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques12" runat="server" Text="Hearing aids."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="0">YES</asp:ListItem>
                    <asp:ListItem Value="1">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES12" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques13" runat="server" Text="Any metal or foreign body."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES13" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques14" runat="server" Text="Transdermal skin patch."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES14" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques15" runat="server" Text="Have you ever had an operation or surgery? Please List All."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="0">YES</asp:ListItem>
                    <asp:ListItem Value="1">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES15" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
         
          <td  colspan="2">
                 <asp:TextBox ID="TXT_LIST" runat="server" TextMode="MultiLine" Height="71px" Width="616px"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques16" runat="server" Text="Are you pregnantor possibly pregnant?PLEASE SEE RECEPTIONIST."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES16" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques17" runat="server" Text="Have you ever been a machinist, metalworker or welder."></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES17" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques18" runat="server" Text="Have you ever been injured by a metallic foreign body in your eye which &nbsp;was not removed?"></asp:Label></td>
          <td width="40%" >
                                  
                 <asp:RadioButtonList ID="RDO_QUES18" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES18" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques19" runat="server" Text="Are you breast-feeding at this time?"></asp:Label></td>
          <td width="40%" >
                                  
                 <asp:RadioButtonList ID="RDO_QUES19" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="0">YES</asp:ListItem>
                    <asp:ListItem Value="1">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES19" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques20" runat="server" Text="Do you, to the best of your knowledge suffer from hemolytic anemia?"></asp:Label></td>
          <td width="40%" >
                 
                 <asp:RadioButtonList ID="RDO_QUES20" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES20" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td width="60%" >&nbsp;<asp:Label ID="lbl_Ques21" runat="server" Text="Do you have a history of kidney or renal abnormalities?"></asp:Label></td>
          <td width="40%" >
                                  
                 <asp:RadioButtonList ID="RDO_QUES21" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">YES</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                 </asp:RadioButtonList>
                 <asp:TextBox ID="TXT_QUES21" runat="server" Visible="false"></asp:TextBox>
          </td>
        </tr>
        <tr style="height:50px">
          <td width="50%"  colspan="2">
            <table width="100%" align="center">
              <tr>
                <td style="height: 22px">&nbsp;<asp:Label ID="lbl_Date" runat="server" Text="Date:"></asp:Label> <asp:TextBox ID="TXT_CURRENT_DATE" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                <td align="right" style="height: 22px"><asp:Label ID="lbl_Technician" runat="server" Text="TECHNICIAN:"></asp:Label>&nbsp;
                    <asp:TextBox ID="TXT_TECHNICIAN" runat="server"></asp:TextBox></td>
              </tr>
            </table>
              <asp:TextBox ID="TXT_CMP_NAME" runat="server" BackColor="Transparent" BorderColor="Transparent"
                  BorderStyle="None" ForeColor="Black" ReadOnly="true" Visible="False"></asp:TextBox></td>
        </tr>
        <tr>
          <td colspan="2" align="center" style="height: 22px">
              &nbsp;<asp:Button ID="btnSubmit" runat="server" CssClass="Buttons" Text="Save" OnClick="btnSubmit_Click" />
               <asp:Button ID="btnSavePrint" runat="server" CssClass="Buttons" Text="Save & Print" OnClick="btnSavePrint_Click"/>
          </td>
        </tr>
      </table>
    </td>
  </tr>
  
</table>
</asp:Content>

