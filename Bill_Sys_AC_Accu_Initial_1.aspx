<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_AC_Accu_Initial_1.aspx.cs" Inherits="Bill_Sys_AC_Accu_Initial" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div class="TDPart"  style="height:auto; width:99%; float:left;">
    <div style="height:10%; width:100%; float:left; font-family:Arial; font-weight:bold; text-align:center;">
    COMPREHENSIVE ACUPUNCTURE REPORT
    <br /><hr style="color:Black; " />       
    </div> 
    
     <div style="height:auto; width:100%; float:left; ">
       <div style="height:auto; width:100%; float:left; margin-top:1%; ">
       <div style="height:auto; width:auto; float:left;">
        <asp:Label ID="lblPatientName" runat="server" CssClass="ContentLabel" Text="Patient's Name" style=" float:left;"></asp:Label>
        <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" CssClass="textboxCSS" style="margin-left:30px;" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" Width="222px"  ></asp:TextBox>
        </div>
         <div style="height:auto; width:auto; float:left; margin-left:10px; ">
        <asp:Label ID="lblDoa" runat="server" CssClass="ContentLabel" Text="Date of Accident" style=" float:left;"  ></asp:Label>
        <asp:TextBox ID="TXT_DOA" runat="server" CssClass="textboxCSS" style="margin-left:30px; float:left;" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" Width="91px" ></asp:TextBox>
        </div> 
        <div style="height:auto; width:auto; float:left ;margin-left:10px;  ">
        <asp:Label ID="Label1" runat="server" CssClass="ContentLabel" Text="Date of Examination" style=" float:left;" ></asp:Label>
        <asp:TextBox ID="TXT_DOE" runat="server" CssClass="textboxCSS" style="margin-left:7px;" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" Width="84px" ></asp:TextBox>
        </div>
        <div style="height:auto; width:auto; float:left;">
           <asp:Label ID="Label2" runat="server" CssClass="ContentLabel" Text="Age" ></asp:Label>
        <asp:TextBox ID="TXT_PATIENT_AGE" runat="server" CssClass="textboxCSS" Width="34px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:TextBox>
         <asp:Label ID="Label3" runat="server" CssClass="ContentLabel" Text="Sex" ></asp:Label>
        <asp:TextBox ID="TXT_PATIENT_SEX" runat="server" CssClass="textboxCSS" Width="60px"  BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:TextBox>
          </div>
        </div>
        
        <div class="ContentLabel" style="height:auto; width:100%; float:left ; margin-top:10px; font-family:Arial;">
       <p>This is an initial report based on the chart notes regarding the acupuncture diagnosis and treatment of the above mentioned patient
          who was involved in a motor vehicle accident. The patient was the</p> 
          <div style="height:auto; width:100%; float:left; text-align:left; margin-top:1%; ">             
            <asp:RadioButtonList ID="RDO_PATIENT_ACCIDENT_CATEGORY" runat="server" RepeatDirection="Horizontal"  style="float:left;" CssClass="ContentLabel" Width="315px" >
            <asp:ListItem Text="driver" Value="0"></asp:ListItem>
            <asp:ListItem Text="Passenger" Value="1"></asp:ListItem>
            <asp:ListItem Text="pedestrian  After the MVA," Value="2"></asp:ListItem>
            </asp:RadioButtonList>            
         
            <asp:RadioButtonList ID="RDO_PATIENT_HOSPITALIZED" runat="server" RepeatDirection="Horizontal"  style="float:left;" CssClass="ContentLabel" Width="254px" >
            <asp:ListItem Text="he was /" Value="0"></asp:ListItem>
            <asp:ListItem Text="was not taken to the hospital." Value="1"></asp:ListItem>     
                 
            </asp:RadioButtonList>    
             
        </div>
        
        <div style="height:auto; width:100%; float:left; text-align:left; margin-top:2%; ">   
            <b style="float:left;">Past medical history</b>
                <div style="height:auto; width:100%; float:left ; margin-top:1%;  ">
                    <asp:Label ID="Label4" runat="server" CssClass="ContentLabel" Text="Medications" ></asp:Label>
                    <asp:TextBox ID="TXT_MEDICATIONS" runat="server" CssClass="textboxCSS" style="margin-left:80px; " ></asp:TextBox>
                </div> 
                 <div style="height:auto; width:100%; float:left ; margin-top:5px;  ">
                    <asp:Label ID="Label5" runat="server" CssClass="ContentLabel" Text="Family history" style="float:left;"  ></asp:Label>
                    <asp:TextBox ID="TXT_FAMILY_HISTORY" runat="server" CssClass="textboxCSS"  style="margin-left:73px;" ></asp:TextBox>
                </div> 
                 <div style="height:auto; width:72%; float:left ; margin-top:5px;  ">
                    <asp:Label ID="Label6" runat="server" CssClass="ContentLabel" Text="Personal medical history" style="float:left;"  ></asp:Label>
                    <asp:TextBox ID="TXT_PERSONAL_MEDICAL_HISTORY" runat="server" CssClass="textboxCSS"  style="margin-left:12px;" ></asp:TextBox>
                </div> 
        </div>
       
        
         </div>
         
            <div style="height:10%; width:100%; float:left; font-family:Arial; font-weight:bold; text-align:left ; margin-top:10px;">
   CURRENT SYMPTOMS        
    </div> 
    <div style="height:auto; width:100%; float:left; text-align:left; margin-top:2%; ">  
     <asp:Label ID="Label7" runat="server" CssClass="ContentLabel" Text="The patient complains of the following symptoms:" ></asp:Label>
        <div style="height:auto; width:100%; float:left;  margin-top:10px; ">
               <asp:Label ID="Label8" runat="server" CssClass="ContentLabel" Text="HEAD" style="float:left; font-weight:bold;" ></asp:Label>
                <div  style="height:auto; width:100%; float:left;  ">  
                <asp:CheckBox ID="CHK_HEADACHES" Text="Headaches" runat="server" Width="17%" style="float:left;"   />
                <asp:RadioButtonList ID="RDO_HEADACHES_LEVEL"  runat="server" RepeatDirection="Horizontal" Width="27%" style="float:left;" CssClass="ContentLabel"  >
                <asp:ListItem Text="Mild" Value="0"></asp:ListItem>
                <asp:ListItem Text="Moderate" Value="1"></asp:ListItem>
                <asp:ListItem Text="Severe" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                </div>
                
                <div  style="height:auto; width:100%; float:left;  ">  
                <asp:CheckBox ID="CHK_DIZZINESS" Text="Dizziness" runat="server" Width="17%" style="float:left;"   />
                <asp:RadioButtonList ID="RDO_DIZZINESS_LEVEL"  runat="server" RepeatDirection="Horizontal" Width="27%" style="float:left;" CssClass="ContentLabel"  >
                <asp:ListItem Text="Mild" Value="0"></asp:ListItem>
                <asp:ListItem Text="Moderate" Value="1"></asp:ListItem>
                <asp:ListItem Text="Severe" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                </div>
                
                <div  style="height:auto; width:100%; float:left;  ">  
                <asp:CheckBox ID="CHK_SLEEPING_DISORDERS" Text="Sleeping disorders" runat="server" Width="17%" style="float:left;"   />
                <asp:RadioButtonList ID="RDO_SLEEPING_DISORDERS_LEVEL"  runat="server" RepeatDirection="Horizontal" Width="27%" style="float:left;" CssClass="ContentLabel"  >
                <asp:ListItem Text="Mild" Value="0"></asp:ListItem>
                <asp:ListItem Text="Moderate" Value="1"></asp:ListItem>
                <asp:ListItem Text="Severe" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                </div>
        </div>
        
        <div style="height:auto; width:100%; float:left;  margin-top:10px; ">
               <asp:Label ID="Label9" runat="server" CssClass="ContentLabel" Text="NECK" style="float:left; font-weight:bold;" ></asp:Label>
                <div  style="height:auto; width:100%; float:left;  ">  
                <asp:CheckBox ID="CHK_NECK_PAIN" Text="Pain" runat="server" Width="17%" style="float:left;"   />
                <asp:RadioButtonList ID="RDO_NECK_PAIN_LEVEL"  runat="server" RepeatDirection="Horizontal" Width="27%" style="float:left;" CssClass="ContentLabel"  >
                <asp:ListItem Text="Mild" Value="0"></asp:ListItem>
                <asp:ListItem Text="Moderate" Value="1"></asp:ListItem>
                <asp:ListItem Text="Severe" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                </div>
                
                <div  style="height:auto; width:100%; float:left;  ">  
                <asp:CheckBox ID="CHK_NECK_STIFFNESS" Text="Stiffness" runat="server" Width="17%" style="float:left;"   />
                <asp:RadioButtonList ID="RDO_NECK_STIFFNESS_LEVEL"  runat="server" RepeatDirection="Horizontal" Width="27%" style="float:left;" CssClass="ContentLabel"  >
                <asp:ListItem Text="Mild" Value="0"></asp:ListItem>
                <asp:ListItem Text="Moderate" Value="1"></asp:ListItem>
                <asp:ListItem Text="Severe" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                </div>
        </div>
        
        <div style="height:auto; width:100%; float:left;  margin-top:10px; ">
               <asp:Label ID="Label10" runat="server" CssClass="ContentLabel" Text="SHOULDER L/R" style="float:left; font-weight:bold;" ></asp:Label>
                <div  style="height:auto; width:100%; float:left;  ">  
                <asp:CheckBox ID="CHK_SHOULDER_PAIN" Text="Pain" runat="server" Width="17%" style="float:left;"   />
                <asp:RadioButtonList ID="RDO_SHOULDER_PAIN_LEVEL"  runat="server" RepeatDirection="Horizontal" Width="27%" style="float:left;" CssClass="ContentLabel"  >
                <asp:ListItem Text="Mild" Value="0"></asp:ListItem>
                <asp:ListItem Text="Moderate" Value="1"></asp:ListItem>
                <asp:ListItem Text="Severe" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                </div>
                
                <div  style="height:auto; width:100%; float:left;  ">  
                <asp:CheckBox ID="CHK_SHOULDER_STIFFNESS" Text="Stiffness" runat="server" Width="17%" style="float:left;"   />
                <asp:RadioButtonList ID="RDO_SHOULDER_STIFFNESS_LEVEL"  runat="server" RepeatDirection="Horizontal" Width="27%" style="float:left;" CssClass="ContentLabel"  >
                <asp:ListItem Text="Mild" Value="0"></asp:ListItem>
                <asp:ListItem Text="Moderate" Value="1"></asp:ListItem>
                <asp:ListItem Text="Severe" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                </div>
        </div>
        
        <div style="height:auto; width:100%; float:left;  margin-top:10px; ">
               <asp:Label ID="Label11" runat="server" CssClass="ContentLabel" Text="BACK" style="float:left; font-weight:bold;" ></asp:Label>
                <div  style="height:auto; width:100%; float:left;  ">  
                <asp:CheckBox ID="CHK_UPPER_BACK_PAIN" Text="Upper back pain" runat="server" Width="17%" style="float:left;"   />
                <asp:RadioButtonList ID="RDO_UPPER_BACK_PAIN_LEVEL"  runat="server" RepeatDirection="Horizontal" Width="27%" style="float:left;" CssClass="ContentLabel"  >
                <asp:ListItem Text="Mild" Value="0"></asp:ListItem>
                <asp:ListItem Text="Moderate" Value="1"></asp:ListItem>
                <asp:ListItem Text="Severe" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                </div>
                
                <div  style="height:auto; width:100%; float:left;  ">  
                <asp:CheckBox ID="CHK_MIDDLE_BACK_PAIN" Text="Middle back pain" runat="server" Width="17%" style="float:left;"   />
                <asp:RadioButtonList ID="RDO_MIDDLE_BACK_PAIN_LEVEL"  runat="server" RepeatDirection="Horizontal" Width="27%" style="float:left;" CssClass="ContentLabel"  >
                <asp:ListItem Text="Mild" Value="0"></asp:ListItem>
                <asp:ListItem Text="Moderate" Value="1"></asp:ListItem>
                <asp:ListItem Text="Severe" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                </div>
                
                <div  style="height:auto; width:100%; float:left;  ">  
                <asp:CheckBox ID="CHK_LOWER_BACK_PAIN" Text="Lower back pain" runat="server" Width="17%" style="float:left;"   />
                <asp:RadioButtonList ID="RDO_LOWER_BACK_PAIN_LEVEL"  runat="server" RepeatDirection="Horizontal" Width="27%" style="float:left;" CssClass="ContentLabel"  >
                <asp:ListItem Text="Mild" Value="0"></asp:ListItem>
                <asp:ListItem Text="Moderate" Value="1"></asp:ListItem>
                <asp:ListItem Text="Severe" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                </div>
                
                <div style="height:auto ; width:100%; float:left;">
                 <asp:Label ID="lblOther" runat="server" CssClass="ContentLabel" Text="Other" ></asp:Label>
                 <asp:TextBox ID="TXT_BACK_OTHER" runat="server" CssClass="textboxCSS" TextMode="MultiLine" Width="73%"  style="margin-left:1%;" Height="93%" ></asp:TextBox>
                </div> 
        </div>
        
           
                <div style="height:auto ; width:100%; float:left; text-align:center; margin-top:15px; ">
                <asp:TextBox ID="txtEventID" runat="server" CssClass="textboxCSS" Visible="false" Width="5px" ></asp:TextBox>
                <asp:Button ID="btnSave" runat="server" Text="Save & Next"  CssClass="Buttons" Width="83px" OnClick="btnSave_Click" />
                </div>
                <div style="height:auto; width:auto; float:left;">
                <asp:TextBox ID="TXT_PATIENT_ACCIDENT_CATEGORY" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                  <asp:TextBox ID="TXT_PATIENT_HOSPITALIZED" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                    <asp:TextBox ID="TXT_HEADACHES_LEVEL" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                      <asp:TextBox ID="TXT_DIZZINESS_LEVEL" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                        <asp:TextBox ID="TXT_SLEEPING_DISORDERS_LEVEL" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                          <asp:TextBox ID="TXT_NECK_PAIN_LEVEL" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                            <asp:TextBox ID="TXT_NECK_STIFFNESS_LEVEL" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                              <asp:TextBox ID="TXT_SHOULDER_PAIN_LEVEL" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                                <asp:TextBox ID="TXT_SHOULDER_STIFFNESS_LEVEL" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                                  <asp:TextBox ID="TXT_UPPER_BACK_PAIN_LEVEL" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                                    <asp:TextBox ID="TXT_MIDDLE_BACK_PAIN_LEVEL" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                                      <asp:TextBox ID="TXT_LOWER_BACK_PAIN_LEVEL" runat="server" Visible="false" Width="6px" ></asp:TextBox>
                    
                </div> 
    </div>
    </div>
    
   
   </div>
</asp:Content>

