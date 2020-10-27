<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AC_AccuReEval.aspx.cs" MasterPageFile="~/MasterPage.master"  Inherits="Bill_Sys_AccuReEval" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div id="wrapper" style="height:auto; width:auto; margin-left:auto; margin-right:auto;  ">
    <div id="container" class="TDPart" style="height:100%; width:100%; float:left;  ">
    <div style="height:20px; width:100%; float:left; text-align:center; font-weight:bold; ">
    ACUPUNCTURE RE - EVALUATION
    </div>
    <table width="100%">
        <tr>
            <td width="25%">
                 <asp:Label ID="lblPatientLastName" runat="server" CssClass="ContentLabel" Text="Patient’s Last Name"></asp:Label>
            </td>
            <td width="25%">
                <asp:TextBox ID="txtPatientLastName" runat="server" CssClass="textboxCSS"  BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
            </td>
            <td width="25%">
                <asp:Label ID="lblFirstName" runat="server" CssClass="ContentLabel"  Text="Patient’s First Name"></asp:Label>
            </td>
            <td width="25%">
                <asp:TextBox ID="txtPatientFirstName" CssClass="textboxCSS" runat="server"  BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td width="25%">
                <asp:Label ID="lblAge" runat="server" Text="Age" CssClass="ContentLabel" ></asp:Label>
            </td>
            <td width="25%">
                <asp:TextBox ID="txtPatientAge" runat="server" CssClass="textboxCSS"  BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
            </td>
            <td width="25%">
                <asp:Label ID="lblSex" runat="server" Text="Sex" CssClass="ContentLabel" ></asp:Label>
            </td>
            <td width="25%">
                <asp:TextBox ID="txtrdosex" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
           <td width="25%">
                <asp:Label ID="lblDateOfAccident" runat="server" CssClass="ContentLabel" Text="Date of Accident"></asp:Label>
            </td>
            <td width="25%">
                <asp:TextBox ID="txtDoa" runat="server" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
            </td>
            <td width="25%">
                <asp:Label ID="lblDateOfExamination" runat="server" CssClass="ContentLabel" Text="Date of Examination"></asp:Label>
            </td>
            <td width="25%">
                <asp:TextBox ID="txtDoe" runat="server" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="height:auto; width:100%; float:left; margin-top:20px; ">
     
     <div  style="height:auto; width:100%; float:left; margin-top:15px; font-weight:bold; font:arial bold 12px; " >
            Objective Findings
    </div>
     <div  style="height:auto; width:85%; float:left; margin-top:15px; margin-left:70px;  " >
     <div style="height:auto;width:100%; float:left; ">
            <asp:Label ID="lblTongue" runat="server" CssClass="ContentLabel" Text="Tongue"></asp:Label>
        <asp:TextBox ID="txtTongue" runat="server" CssClass="textboxCSS" style="margin-left:5px;" Width="349px"></asp:TextBox>
        </div>
        
        <div style="height:auto;width:100% ; margin-top:5px; float:left; ">
            <asp:Label ID="lblCoat" runat="server" CssClass="ContentLabel" Text="Coat"></asp:Label>
        <asp:TextBox ID="txtCoat" runat="server" CssClass="textboxCSS" style="margin-left:20px;" Width="349px"></asp:TextBox>
        </div>
        
        <div style="height:auto;width:100% ; margin-top:5px; float:left; ">
            <asp:Label ID="lblPulse" runat="server" CssClass="ContentLabel" Text="Pulse"></asp:Label>
        <asp:TextBox ID="txtPulse" runat="server" CssClass="textboxCSS" style="margin-left:15px;" Width="349px"></asp:TextBox>
        </div>

    </div>
     <div  style="height:auto; width:100%; float:left; margin-top:15px; font-weight:bold; font:arial 12px; " >
            Current Complaints
    </div>
      <div style="height:auto; text-align:left; width:100%; float:left; margin-top:15px;  " >
         <div style="height:auto;width:100% ; float:left; ">
             <asp:CheckBox ID="chkHead"  CssClass="ContentLabel" runat="server" Text="Head" />
             <asp:CheckBox ID="chkChest"  CssClass="ContentLabel" runat="server" Text="Chest" style="padding-left:80px;" />
             <asp:CheckBox ID="chkKneeLeftRight" runat="server"  CssClass="ContentLabel" Text="Knee L/R" style="margin-left:80px;"  />
             <asp:CheckBox ID="chkNeck" runat="server" CssClass="ContentLabel" Text="Neck" style="margin-left:60px;" />
             <asp:CheckBox ID="chkHipLeftRight" runat="server" CssClass="ContentLabel" Text="Hip L/R" style="margin-left:83px;" />
        </div>
        
       <div  style="height:auto;width:100% ; float:left; text-align:left; margin-top:15px; ">
             <%--<asp:CheckBox ID="chkNeck" runat="server" CssClass="ContentLabel" Text="Neck" />
             <asp:CheckBox ID="chkHipLeftRight" runat="server" CssClass="ContentLabel" Text="Hip L/R" style="margin-left:83px;" />
            --%> 
            <asp:CheckBox ID="chkUpperLegLeftRight" runat="server" CssClass="ContentLabel" Text="Upper Leg L/R" /> 
            <asp:CheckBox ID="chkLowerBack" runat="server" CssClass="ContentLabel" Text="Lower back" style="padding-left:30px;"/>
             <asp:CheckBox ID="chkElbowLeftRight" runat="server" CssClass="ContentLabel" Text="Elbow L/R" style="margin-left:50px;" />
             <asp:CheckBox ID="chkAnkleLeftRight" runat="server" CssClass="ContentLabel" Text="Ankle L/R" style="margin-left:55px;"  />
             <asp:CheckBox ID="chkShoulderLeftRight" runat="server" CssClass="ContentLabel" Text="Shoulder L/R" style="margin-left:60px;" />
        </div>
         <div style="height:auto;width:100% ; float:left; margin-top:15px; ">
             <asp:CheckBox ID="chkUpperMiddleBack" runat="server" CssClass="ContentLabel" Text="Upper/Mid back" />
             <asp:CheckBox ID="chkArmLeftRight" runat="server" CssClass="ContentLabel" Text="Arm L/R" style="margin-left:25px; " />
             <asp:CheckBox ID="chkLowerLegLeftRight" runat="server" CssClass="ContentLabel" Text="Lower Leg L/R" style="margin-left:68px; "  />
                          <asp:CheckBox ID="chkFootLeftRight" runat="server" CssClass="ContentLabel" Text="Foot L/R" style="margin-left:30px;"  />
             <asp:CheckBox ID="chkScapulaLeftRight" runat="server" CssClass="ContentLabel" Text="Scapula L/R" style="margin-left:70px;" />

        </div>
        
           <div style="height:auto;width:100% ; float:left; margin-top:15px; ">
           
           <asp:CheckBox ID="chkWristLeftRight" runat="server" CssClass="ContentLabel" Text="Wrist L/R" />
             <asp:CheckBox ID="chkHankLeftRight" runat="server" CssClass="ContentLabel" Text="Hand L/R" style="margin-left:59px;" />
             <asp:CheckBox ID="chkCurrentComplaintOther" runat="server" CssClass="ContentLabel" Text="Other" style="margin-left:60px;"  />
             
        </div>

    </div>
    
    <div  style="height:auto; width:100%; float:left; margin-top:15px; font-weight:bold; font:arial bold 12px; " >
            Channel Involved
    </div>
      <div  style="height:auto; width:100%; float:left; margin-top:15px;" >
        
       <div style="height:auto;width:100% ; float:left;  ">
             <asp:CheckBox ID="chkLargeIntestine" runat="server" CssClass="ContentLabel" Text="Large intestine" />
             <asp:CheckBox ID="chkSmallIntestine" runat="server" CssClass="ContentLabel" Text="Small intestine" style="margin-left:34px;" />
             <asp:CheckBox ID="chkSanJiao" runat="server" CssClass="ContentLabel" Text="San jiao" style="margin-left:29px;"  />
              <asp:CheckBox ID="chkLung" runat="server" CssClass="ContentLabel" Text="Lung"  style="margin-left:60px;"  />
             <asp:CheckBox ID="chkHeart" runat="server" CssClass="ContentLabel" Text="Heart" style="padding-left:88px;" />

        </div>
        
         <div style="height:auto;width:100% ; float:left; margin-top:15px; ">
             <asp:CheckBox ID="chkStomach" runat="server" CssClass="ContentLabel" Text="Stomach" />
             <asp:CheckBox ID="chkBladder" runat="server" CssClass="ContentLabel" Text="Bladder" style="margin-left:68px;" />
             <asp:CheckBox ID="chkGallbladder" runat="server" CssClass="ContentLabel" Text="Gallbladder" style="margin-left:67px;"  />
              <asp:CheckBox ID="chkSpleen" runat="server" CssClass="ContentLabel" Text="Spleen"  style="margin-left:40px;" />
             <asp:CheckBox ID="chkKidney" runat="server" CssClass="ContentLabel" Text="Kidney" style="margin-left:78px;" />

        </div>
        
          <div style="height:auto;width:100% ; float:left; margin-top:15px; ">
                       <asp:CheckBox ID="chkPericardium" runat="server" CssClass="ContentLabel" Text="Pericardium"   />

             <asp:CheckBox ID="chkLiver" runat="server" CssClass="ContentLabel" Text="Liver" style="margin-left:50px;" Width="47px"  />
             <asp:CheckBox ID="chkChannelInvolvedOther" CssClass="ContentLabel" runat="server" Text="Other" style="margin-left:80px;"  />

        </div>
        
    </div>
    
    <div  style="height:auto; width:100%; float:left; margin-top:15px; font-weight:bold; font:arial bold 12px; " >
            Patient Condition
    </div>
      <div  style="height:auto; width:100%; float:left; margin-top:15px;  " >
         <div style="height:auto;width:100% ; float:left; ">
             <asp:CheckBox ID="chkImproved" CssClass="ContentLabel" runat="server" Text="Improved"  />
            <asp:CheckBox ID="chkWorsened" CssClass="ContentLabel" runat="server" Text="Worsened"  style="margin-left:65px;"/>
             <asp:CheckBox ID="chkNoChange" CssClass="ContentLabel" runat="server" Text="No Change" style="margin-left:50px;" />


        </div>
        
       
        
         <div style="height:auto;width:100% ; float:left; margin-top:15px; ">
             <asp:CheckBox ID="chkDcmDx" runat="server" CssClass="ContentLabel" Text="TCM DX" />
             <asp:TextBox ID="txtTcmBx" runat="server" CssClass="textboxCSS" style="margin-left:5px;" Width="557px"></asp:TextBox>
        </div>
        
         
        
    </div>
    
    <div  style="height:auto; width:100%; float:left; margin-top:15px; font-weight:bold; font:arial bold 12px;" >
            Treatment Plan
    </div>
    
       <div  style="height:auto; width:100%; float:left; margin-top:15px;" >
     
         <div style="height:auto;width:100% ; float:left;  ">
             <asp:CheckBox ID="chkContinueTreatment" runat="server" CssClass="ContentLabel" Text="Continue treatment" />            
             <asp:TextBox ID="txtPerWk" runat="server" CssClass="textboxCSS"  style="margin-left:15px;" Width="80px"></asp:TextBox>
             <asp:Label ID="lblPerWk" runat="server" CssClass="ContentLabel" Text="per wk" style="margin-left:5px;"></asp:Label>
             <asp:TextBox ID="txtWeeks" runat="server" CssClass="textboxCSS"   style="margin-left:15px;" Width="80px"></asp:TextBox>
             <asp:Label ID="lblWeeks" runat="server" CssClass="ContentLabel" Text="weeks" style="margin-left:5px;"></asp:Label>
        </div>
        
          <div style="height:auto;width:100% ; float:left;margin-top:15px;">
             <asp:CheckBox ID="chkDischargePatient" runat="server" CssClass="textboxCSS"  Text="Dischage Pt." style="" />            
                            <asp:TextBox ID="txtDoctorName" runat="server" CssClass="textboxCSS" style="float:left; margin-right:10%;" Visible="False" Width="16px" ></asp:TextBox>
             <asp:TextBox id="txt_PATIENT_ID" runat="server" CssClass="textboxCSS" Text="5" Visible="False" Width="19px"></asp:TextBox>

        </div>
        
        <div style="height:auto; width:100%; float:left; text-align:center;">
            <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="Buttons" OnClick="btnSave_Click" Width="80px"  />
        </div>
        
    </div>
    </div>
    

</asp:Content> 