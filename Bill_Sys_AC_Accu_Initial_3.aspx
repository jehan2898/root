<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_AC_Accu_Initial_3.aspx.cs" Inherits="Bill_Sys_AC_Accu_Initial_3" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" class="TDPart">
<tr>
<td style="width:99%; height: 24px;" > 
<div style="height:auto; width:100%; float:left; margin-top:1%; ">
       <div style="height:auto; width:auto; float:left;">
        <asp:Label ID="lblPatientName" runat="server" CssClass="ContentLabel" Text="Patient's Name" style=" float:left;"></asp:Label>
        <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" CssClass="textboxCSS" style="margin-left:33px;" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" Width="222px" ></asp:TextBox>
        </div>
         <div style="height:auto; width:auto; float:left; margin-left:10px; ">
        <asp:Label ID="lblDoa" runat="server" CssClass="ContentLabel" Text="Date of Accident" style=" float:left;"  ></asp:Label>
        <asp:TextBox ID="TXT_DOA" runat="server" CssClass="textboxCSS" style="margin-left:30px; float:left;" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" Width="91px" ></asp:TextBox>
        </div> 
        <div style="height:auto; width:auto; float:left ;margin-left:10px;  ">
        <asp:Label ID="Label19" runat="server" CssClass="ContentLabel" Text="Date of Examination" style=" float:left;" ></asp:Label>
        <asp:TextBox ID="TXT_DOE" runat="server" CssClass="textboxCSS" style="margin-left:7px;" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" Width="87px" ></asp:TextBox>
        </div>
        <div style="height:auto; width:auto; float:left;margin-left:10px;  ">
           <asp:Label ID="Label20" runat="server" CssClass="ContentLabel" Text="Age" ></asp:Label>
        <asp:TextBox ID="TXT_PATIENT_AGE" runat="server" CssClass="textboxCSS" Width="34px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:TextBox>
         <asp:Label ID="Label21" runat="server" CssClass="ContentLabel" Text="Sex" ></asp:Label>
        <asp:TextBox ID="TXT_PATIENT_SEX" runat="server" CssClass="textboxCSS" Width="60px"  BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:TextBox>
          </div>
        </div>
        </table> 
<table  border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: auto; float:left; " class="TDPart">
<tr>
     <td align="center" style="width:900px;" >
        <b> INITIAL IMPRESSION</b>
    </td>
</tr>
<tr>
     <td style="height:10%; width: 900px;" >
        
    </td>
</tr>
<tr>
<td style="width: 900px; height: 184px;">
<table style="width: 97%">
<tr>
    <td  >
        <asp:CheckBox ID="CHK_SHOULDER_JOINT_PAIN_719_41" runat="server" Text="719.41" CssClass="ContentLabel" />
         <asp:Label ID="lbl1" runat="server" Text="Pain in Joint: sholuder L/R"></asp:Label>  
    </td>
    <td >
        <asp:CheckBox ID="CHK_ELBOW_JOINT_PAIN_719_42" runat="server" Text="719.42" CssClass="ContentLabel" />
         <asp:Label ID="Label1" runat="server" Text="Pain in Joint: elbow L/R"></asp:Label>  
    </td>
     <td >
        <asp:CheckBox ID="CHK_WRIST_JOINT_PAIN_719_43" runat="server" Text="719.43" CssClass="ContentLabel" />
         <asp:Label ID="Label2" runat="server" Text="Pain in Joint: wrist L/R"></asp:Label>  
    </td>
      <td >
        <asp:CheckBox ID="CHK_HAND_JOINT_PAIN_719_44" runat="server" Text="719.44" CssClass="ContentLabel" />
         <asp:Label ID="Label3" runat="server" Text="Pain in Joint: hand L/R"></asp:Label>  
    </td>
   
</tr>

<tr>
    <td  >
        <asp:CheckBox ID="CHK_HIP_PELVIC_JOINT_PAIN_719_45" runat="server" Text="719.45" CssClass="ContentLabel" />
         <asp:Label ID="Label4" runat="server" Text="Pain in Joint: hip,pelvic region"></asp:Label>  
    </td>
    <td >
        <asp:CheckBox ID="CHK_KNEE_JOINT_PAIN_719_46" runat="server" Text="719.46" CssClass="ContentLabel" />
         <asp:Label ID="Label5" runat="server" Text="Pain in Joint: knee"></asp:Label>  
    </td>
     <td >
        <asp:CheckBox ID="CHK_ANKLE_FOOT_JOINT_PAIN_719_47" runat="server" Text="719.47" CssClass="ContentLabel" />
         <asp:Label ID="Label6" runat="server" Text="Pain in Joint: ankle,foot L/R"></asp:Label>  
    </td>
      <td >
        <asp:CheckBox ID="CHK_LIMP_PAIN_729_5" runat="server" Text="729.5" CssClass="ContentLabel" />
         <asp:Label ID="Label7" runat="server" Text="Pain in limp L/R"></asp:Label>  
    </td>
   
</tr>

<tr>
    <td  >
        <asp:CheckBox ID="CHK_LUMBARGO_724_2" runat="server" Text="724.2" CssClass="ContentLabel" />
         <asp:Label ID="Label8" runat="server" Text="Lumbargo (low back pain)"></asp:Label>  
    </td>
    <td >
        <asp:CheckBox ID="CHK_CERVICALGIA_723_1" runat="server" Text="723.1" CssClass="ContentLabel" />
         <asp:Label ID="Label9" runat="server" Text="Cervicalgia (neck pain)" Width="134px"></asp:Label>  
    </td>
     <td >
        <asp:CheckBox ID="CHK_THORACIC_847_1" runat="server" Text="847.1" CssClass="ContentLabel" />
         <asp:Label ID="Label10" runat="server" Text="Thoracic area(upper/mid back pain)"></asp:Label>  
    </td>
      <td >
        <asp:CheckBox ID="CHK_THORACIC_SPINE_724_1" runat="server" Text="724.1" CssClass="ContentLabel" />
         <asp:Label ID="Label11" runat="server" Text="Thoracic spine pain"></asp:Label>  
    </td>
   
</tr>

<tr>
    <td  >
        <asp:CheckBox ID="CHK_MYOFASCIAL_PAIN_729_1" runat="server" Text="729.1" CssClass="ContentLabel" />
         <asp:Label ID="Label12" runat="server" Text="Myofascial pain"></asp:Label>  
    </td>
    <td >
        <asp:CheckBox ID="CHK_CHEST_PAIN_786_5" runat="server" Text="786.5" CssClass="ContentLabel" />
         <asp:Label ID="Label13" runat="server" Text="Chest pain"></asp:Label>  
    </td>
     <td >
        <asp:CheckBox ID="CHK_DIZZINESS_780_4" runat="server" Text="780.4" CssClass="ContentLabel" />
         <asp:Label ID="Label14" runat="server" Text="Dizziness"></asp:Label>  
    </td>
      <td >
        <asp:CheckBox ID="CHK_HEADACHES_784_0" runat="server" Text="784.0" CssClass="ContentLabel" />
         <asp:Label ID="Label15" runat="server" Text="Headaches"></asp:Label>  
    </td>
   
</tr>

<tr>
    <td  >
        <asp:CheckBox ID="CHK_ANXIETY_300_2" runat="server" Text="300.2" CssClass="ContentLabel" />
         <asp:Label ID="Label16" runat="server" Text="Anxiety"></asp:Label>  
    </td>

   
</tr>




</table>
</td>
</tr>
<tr>
    <td style="width: 900px" >
       <asp:Label ID="Label17" runat="server" Text="Treatment And Recommendation" CssClass="ContentLabel"></asp:Label>  
    </td>

   
</tr>
<tr>
    <td style="width: 900px" >
      <asp:TextBox ID="TXT_TREATMENT_AND_RECOMMENDATION" runat="server" CssClass="textboxCSS" Width="96%"  TextMode="MultiLine" ></asp:TextBox>
    </td>

   
</tr>

<tr>
    <td style="width: 900px" >
       <asp:Label ID="Label18" runat="server" Text="Prognosis" CssClass="ContentLabel"></asp:Label>  
    </td>

   
</tr>
<tr>
    <td style="width: 900px" >
      <asp:TextBox ID="TXT_PROGNOSIS" runat="server" CssClass="textboxCSS" Width="96%"  TextMode="MultiLine" ></asp:TextBox>
    </td>

   
</tr>

<tr>
    <td style="height:15px; width: 900px;" >
     
    </td>

   
</tr>

<tr>
    <td  align="center" style="width: 900px"  >
    <asp:TextBox ID="txtEventID" runat="server" CssClass="textboxCSS" Visible="false" Width="5px" ></asp:TextBox>
    <asp:Button ID="btnPrevious" runat="server" CssClass="Buttons" Text="Previous" Width="80px" OnClick="btnPrevious_Click"  />
        <asp:Button ID="btnSave" runat="server" CssClass="Buttons" Text="Save" Width="80px" OnClick="btnSave_Click" />
    </td>

   
</tr>
 <tr>
 <td style="height:15px; width: 900px;"></td>
 </tr>
</table> 

</asp:Content>

