<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_AC_Accu_Initial_2.aspx.cs" Inherits="Bill_Sys_AC_Accu_Initial_2" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="TDPart"  style="height:auto; width:100%; float:left; margin-top:1%; ">
       <div style="height:auto; width:auto; float:left;">
        <asp:Label ID="lblPatientName" runat="server" CssClass="ContentLabel" Text="Patient's Name" style=" float:left;"></asp:Label>
        <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" CssClass="textboxCSS" style="margin-left:33px;" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" Width="222px" ></asp:TextBox>
        </div>
         <div style="height:auto; width:auto; float:left; margin-left:10px; ">
        <asp:Label ID="lblDoa" runat="server" CssClass="ContentLabel" Text="Date of Accident" style=" float:left;"  ></asp:Label>
        <asp:TextBox ID="TXT_DOA" runat="server" CssClass="textboxCSS" style="margin-left:30px; float:left;" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" Width="91px" ></asp:TextBox>
        </div> 
        <div style="height:auto; width:auto; float:left ;margin-left:10px;  ">
        <asp:Label ID="Label9" runat="server" CssClass="ContentLabel" Text="Date of Examination" style=" float:left;" ></asp:Label>
        <asp:TextBox ID="TXT_DOE" runat="server" CssClass="textboxCSS" style="margin-left:7px;" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" Width="87px" ></asp:TextBox>
        </div>
        <div style="height:auto; width:auto; float:left;margin-left:10px;  ">
           <asp:Label ID="Label10" runat="server" CssClass="ContentLabel" Text="Age" ></asp:Label>
        <asp:TextBox ID="TXT_PATIENT_AGE" runat="server" CssClass="textboxCSS" Width="34px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:TextBox>
         <asp:Label ID="Label11" runat="server" CssClass="ContentLabel" Text="Sex" ></asp:Label>
        <asp:TextBox ID="TXT_PATIENT_SEX" runat="server" CssClass="textboxCSS" Width="60px"  BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:TextBox>
          </div>
        </div>
<table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: auto; float:left; " class="TDPart">
<tr>
    <td  class="ContentLabel" style="width:100%; text-align:left ; font-size:15px;">
       <b>TCM DIAGNOSIS</b> 
    </td>
     
</tr>
 <tr>
 <td style="height:15px;"></td>
 </tr>
<tr style="margin-top:10px; ">
   <td  class="ContentLabel" style="width:100%; text-align:left ; font-size:12px; height: 16px;">
       <b>Tongue</b> 
    </td>
</tr>
<tr>
    <td>
        <table  style="width: 100%; height: auto; float:left; margin-left:10px;  " >
            <tr>
                <td style="margin-left:1%; width:5%;" >
                   <asp:Label ID="lblcolor" runat="server" CssClass="ContentLabel" Text="Color :"></asp:Label>
                </td>
                <td style="margin-left:1%;" valign="middle"  >
                   <asp:RadioButtonList ID="RDO_TONGUE_COLOR" runat="server" CssClass="ContentLabel" RepeatDirection="Horizontal" style="margin-top:20px;" RepeatLayout="Flow">
                    <asp:ListItem Text="Regular" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Red" Value="1"></asp:ListItem>
                    <asp:ListItem Text="White" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Yellow" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Bluish" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Purple" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Other" Value="6"></asp:ListItem>
                   </asp:RadioButtonList>
                    
                   <asp:TextBox ID="TXT_TONGUE_COLOR_OTHER" runat="server" CssClass="textboxCSS" Width="249px"></asp:TextBox>
                   
                </td>
                
            </tr>
           <tr>
           <td></td>
            <td>
                
            </td>
           </tr>
        </table>
    </td>
</tr>

<tr style="margin-top:10px; ">
   <td  class="ContentLabel" style="width:100%; text-align:left ; font-size:12px; height: 16px;">
       <b>Coating</b> 
    </td>
</tr>
<tr>
    <td>
        <table  style="width: 100%; height: auto; float:left; margin-left:10px; " >
            <tr>
                <td style="margin-left:1%; width:5%;" >
                   <asp:Label ID="Label2" runat="server" CssClass="ContentLabel" Text="Color :"></asp:Label>
                </td>
                <td style="margin-left:1%;" valign="middle"  >
                   <asp:RadioButtonList ID="RDO_COATING_COLOR" runat="server" CssClass="ContentLabel" RepeatDirection="Horizontal" style="margin-top:15px;" RepeatLayout="Flow">
                    <asp:ListItem Text="White" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Yellow" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Black" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Gray" Value="3"></asp:ListItem>
                    <asp:ListItem Text="None" Value="4"></asp:ListItem>
                    
                   </asp:RadioButtonList>
                   
                </td>
            </tr>
            
            <tr>
                <td style="margin-left:1%; width:5%;" >
                   <asp:Label ID="Label3" runat="server" CssClass="ContentLabel" Text="Thickness :"></asp:Label>
                </td>
                <td style="margin-left:1%;" valign="middle"  >
                   <asp:RadioButtonList ID="RDO_COATING_THICKNESS" runat="server" CssClass="ContentLabel" RepeatDirection="Horizontal" style="margin-top:15px;" RepeatLayout="Flow">
                    <asp:ListItem Text="Ordinary" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Thick" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Thin" Value="2"></asp:ListItem>
                    <asp:ListItem Text="None" Value="3"></asp:ListItem>                   
                   </asp:RadioButtonList>
                   
                </td>
            </tr>
            
            <tr>
            </tr>
           
        </table>
                 <table  style="width: 100%; height: auto; float:left; " >
                 <tr style="margin-top:10px; ">
   <td  class="ContentLabel" style=" text-align:left ; font-size:12px; height: 16px;">
       <b>Pulse</b> 
    </td>
</tr>
            <tr style="margin-left:10px; ">
                <td style="margin-left:10px; width:5%; " >
                   <asp:Label ID="Label4" runat="server" CssClass="ContentLabel" Text=" Pulse"></asp:Label>
                </td>
                <td style="margin-left:10px;" valign="middle"  >
                   <asp:RadioButtonList ID="RDO_PULSE" runat="server" CssClass="ContentLabel" RepeatDirection="Horizontal" style="margin-top:15px;" RepeatLayout="Flow">
                    <asp:ListItem Text="Regular" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Weak" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Tense" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Full" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Thread" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Slippery" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Uneven" Value="6"></asp:ListItem>
                     <asp:ListItem Text="Rapid" Value="7"></asp:ListItem>
                      <asp:ListItem Text="Slow" Value="8"></asp:ListItem>
                      <asp:ListItem Text="Other" Value="9"></asp:ListItem>
                   </asp:RadioButtonList>
                   
                 
                   <asp:TextBox ID="TXT_PULSE_OTHER" runat="server" CssClass="textboxCSS" Width="249px"></asp:TextBox>
                   
                </td>
            </tr>
            <tr>
            <td>
            </td>
            
            <td>
                 
            </td>
            </tr>
           
        </table>
    </td>
</tr>
 <tr>
<td style="height:15px;"></td>
 </tr>
<tr>
    <td  class="ContentLabel" style="width:100%; text-align:left ; font-size:15px; top:2%;">
       <b>OBJECTIVE FINDING</b> 
    </td>
     
</tr>
 <tr>
 <td style="height:15px;"></td>
 </tr>
<tr style="margin-top:10px; ">
   <td  class="ContentLabel" style="width:100%; text-align:left ; font-size:12px; height: 16px;">
       <b>Muscle Tension</b> 
    </td>
</tr>
 <tr>
               
                <td style="margin-left:1%; height: 9px;" valign="middle"  >
                   <asp:RadioButtonList ID="RDO_MUSCLE_TENSION" runat="server" CssClass="ContentLabel" RepeatDirection="Horizontal" style="margin-top:15px;" RepeatLayout="Flow">
                    <asp:ListItem Text="Neck" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Shoulder R/L" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Middle back" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Lower back" Value="3"></asp:ListItem>                   
                   </asp:RadioButtonList>
                   
                </td>
            </tr>
  
  <tr style="margin-top:10px; ">
   <td  class="ContentLabel" style="width:100%; text-align:left ; font-size:12px; height: 16px;">
       <b>Motion</b> 
    </td>
</tr>
<tr>
<td>
  <table  style="width: 100%; height: auto; float:left; " >
                
            <tr>
                <td style="margin-left:1%; width:7%;" >
                   <asp:Label ID="Label6" runat="server" CssClass="ContentLabel" Text="Neck"></asp:Label>
                </td>
                <td style="margin-left:1%;" valign="middle"  >
                   <asp:RadioButtonList ID="RDO_NECK_MOTION" runat="server" CssClass="ContentLabel" RepeatDirection="Horizontal" style="margin-top:15px;" RepeatLayout="Flow">
                    <asp:ListItem Text="Restricted" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Not restricted" Value="1"></asp:ListItem>
                   
                   </asp:RadioButtonList>
                  
                </td>
            </tr>
            
                 <tr>
                <td style="margin-left:1%; width:7%;" >
                   <asp:Label ID="Label7" runat="server" CssClass="ContentLabel" Text="Back"></asp:Label>
                </td>
                <td style="margin-left:1%;" valign="middle"  >
                   <asp:RadioButtonList ID="RDO_BACK_MOTION" runat="server" CssClass="ContentLabel" RepeatDirection="Horizontal" style="margin-top:15px;" RepeatLayout="Flow">
                    <asp:ListItem Text="Restricted" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Not restricted" Value="1"></asp:ListItem>
                   
                   </asp:RadioButtonList>
                  
                </td>
            </tr>
            
                       <tr>
                <td style="margin-left:1%; width:7%;" >
                   <asp:Label ID="Label8" runat="server" CssClass="ContentLabel" Text="Shoulder L/R"></asp:Label>
                </td>
                <td style="margin-left:1%;" valign="middle"  >
                   <asp:RadioButtonList ID="RDO_SHOULDER_LEFT_RIGHT_MOTION" runat="server" CssClass="ContentLabel" RepeatDirection="Horizontal" style="margin-top:15px;" RepeatLayout="Flow">
                    <asp:ListItem Text="Restricted" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Not restricted" Value="1"></asp:ListItem>
                   
                   </asp:RadioButtonList>
                  
                </td>
            </tr>
           
        </table>
   </td>
    </tr>
    
    <tr style="margin-top:10px; ">
   <td  class="ContentLabel" style="width:100%; text-align:left ; font-size:12px; height: 16px;">
       <b>Myofascial Trigger Point</b> 
    </td>
</tr>
<tr>
<td>
<table style="width: auto; height: auto; float:left; " >
 <tr>
               
                <td style="margin-left:1%; width:auto;" valign="middle">
                 <asp:CheckBox ID="CHK_NECK_TRIGGER_POINT" runat="server" Text="Neck" CssClass="ContentLabel" />
                 </td>
                <td>
                 <asp:CheckBox ID="CHK_SHOULDER_TRIGGER_POINT" runat="server" Text="Left / Right Shoulder" CssClass="ContentLabel" />
                 </td>
                 <td>
                 <asp:CheckBox ID="CHK_UPPER_BACK_TRIGGER_POINT" runat="server" Text="Left / Right upper back" CssClass="ContentLabel" />
                 </td>
                 <td>
                 <asp:CheckBox ID="CHK_MIDDLE_BACK_TRIGGER_POINT" runat="server" Text="Left / Right middle back" CssClass="ContentLabel" />
                 </td>
                 <td>
                 <asp:CheckBox ID="CHK_LOWER_BACK_TRIGGER_POINT" runat="server" Text="Left / Right lower back" CssClass="ContentLabel" />
                 </td>
                 <td>
                 <asp:CheckBox ID="CHK_LEG_TRIGGER_POINT" runat="server" Text="Left / Right leg" CssClass="ContentLabel" />
                 </td>
               
                     
                
            </tr>
  </table>
  </td>
 </tr>
 <tr>
 <td style="height:15px;">
                 <asp:TextBox ID="TXT_TONGUE_COLOR" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                <asp:TextBox ID="TXT_COATING_COLOR" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                <asp:TextBox ID="TXT_COATING_THICKNESS" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                <asp:TextBox ID="TXT_PULSE" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                <asp:TextBox ID="TXT_MUSCLE_TENSION" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                <asp:TextBox ID="TXT_NECK_MOTION" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                <asp:TextBox ID="TXT_BACK_MOTION" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                <asp:TextBox ID="TXT_SHOULDER_LEFT_RIGHT_MOTION" runat="server" Visible="false" Width="6px" ></asp:TextBox>

 </td>
 </tr>
 <tr  >
    <td  align="center"  >
     <asp:TextBox ID="txtEventID" runat="server" CssClass="textboxCSS" Visible="false" Width="5px" ></asp:TextBox>
         <asp:Button ID="btnPrevious" runat="server" CssClass="Buttons" Text="Previous" Width="80px" OnClick="btnPrevious_Click"  />

        <asp:Button ID="btnSave" runat="server" CssClass="Buttons" Text="Save & Next" OnClick="btnSave_Click" />
    </td>
 </tr>
  <tr>
 <td style="height:15px;"></td>
 </tr>
    </table> 
</asp:Content>

