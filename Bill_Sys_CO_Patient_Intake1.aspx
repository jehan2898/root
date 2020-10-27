<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_CO_Patient_Intake1.aspx.cs" Inherits="Bill_Sys_CO_Patient_Intake1" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table class="ContentTable" width="100%">
        <tr>
            <td class="TDPart" align="center" valign="top">
                <table width="100%">
                    <tr>
                        <td align="center" style="width: 921px">
                            <asp:Label ID="lblFormHeader" runat="server" Text="PATIENT  INTAKE - (PAGE  I of IV)" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 921px">
                        </td>
                    </tr>
                </table>
                <table width="99%">
                    <tbody>
                        <tr>
                            <td align="left" valign="middle" style="width: 100%" >
                               <%-- <asp:Label ID="lbl_CaseId" runat="server" Text="Case Id" Width="108px" Visible="False"></asp:Label>--%>
                                <asp:Label ID="lbl_ReferredBy" runat="server" Text="Referred By" Width="92px"></asp:Label></td>
                            
                            <td align="left" valign="top" style="width: 100%" colspan="5" >
                                &nbsp;<asp:TextBox ID="TXT_REFERRED_BY" runat="server" Width="254px"></asp:TextBox></td>
                            <td align="left" valign="middle" style="width: 232px">
                                </td>
                            <td align="center" valign="middle" style="width: 9px">
                                </td>
                            <td align="left" valign="middle" style="width: 232px">
                                </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="height: 25px; width: 100%;" colspan="" >
                                <asp:Label ID="lbl_FirstName" runat="server" Text="Patient First Name" Width="171px"></asp:Label></td>
                           
                            <td align="left" valign="top" style="width: 225px; height: 25px;" >
                                <asp:TextBox ID="TXT_PATIENT_FIRST_NAME" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_PATIENT_FIRST_NAME1" runat="server" Width="1px"></asp:Label>
                                </td>
                            <td align="left" valign="middle" style="height: 25px; width: 131px;"></td>
                               
                            <td align="center" valign="middle" style="width: 9px; height: 25px;">
                                </td>
                            <td align="left" valign="middle" style="width: 232px; height: 25px;">
                                
                                </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="width: 214px" colspan="" >
                                 <asp:Label ID="lbl_LastName" runat="server" Text="Patient Last Name" Width="174px"></asp:Label>
                            </td>
                            
                            <td align="left" valign="top">
                                <asp:TextBox ID="TXT_PATIENT_LAST_NAME" runat="server"  Visible="False" Width="1px"></asp:TextBox>
                                <asp:Label ID="TXT_PATIENT_LAST_NAME1" runat="server" Width="131px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="width: 214px" colspan="" >
                                <asp:Label ID="lbl_MI" runat="server" Text="MI" Width="157px"></asp:Label></td>
                           
                            <td align="left" valign="top" style="width: 225px" >
                                <asp:TextBox ID="TXT_PATIENT_MI" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                 <asp:Label ID="TXT_PATIENT_MI1" runat="server" Width="1px"></asp:Label>
                                
                            </td>
                                
                            <td align="left" valign="middle" style="width: 131px">
                                <asp:Label ID="lbl_Title" runat="server" Text="Title"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                </td>
                            <td align="left" valign="middle" style="width: 232px">
                                <asp:TextBox ID="TXT_PATIENT_TITLE" runat="server" Width="152px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="width: 214px" >
                                <asp:Label ID="lbl_Address" runat="server" Text="Address"></asp:Label></td>
                            
                            <td align="left" valign="top" style="width: 225px" >
                                <asp:TextBox ID="TXT_PATIENT_ADDRESS" runat="server" Visible="False" Width="1px"></asp:TextBox>
                                <asp:Label ID="TXT_PATIENT_ADDRESS1" runat="server"></asp:Label>                                
                                </td>
                                
                            <td align="left" valign="top" style="width: 131px">
                                <asp:Label ID="lbl_Patient_City" runat="server" Text="City"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                            </td>
                            <td align="left" valign="top" style="width: 232px">
                                <asp:TextBox ID="TXT_PATIENT_CITY" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_PATIENT_CITY1" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="width: 214px; height: 22px;" >
                                <asp:Label ID="lbl_PatientState" runat="server" Text="State"></asp:Label></td>
                            
                            <td align="left" valign="top" style="width: 225px; height: 22px;" >
                                <asp:TextBox ID="TXT_PATIENT_STATE" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                 <asp:Label ID="TXT_PATIENT_STATE1" runat="server" ></asp:Label>    
                            </td>
                            <td align="left" valign="middle" style="width: 131px">
                                <asp:Label ID="lbl_PatientZipCode" runat="server" Text="Zip Code" Width="122px"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="top" style="width: 232px; height: 22px;">
                                <asp:TextBox ID="TXT_PATIENT_ZIP" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_PATIENT_ZIP1" runat="server"></asp:Label>    
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" >
                                <asp:Label ID="lbl_HomePhoneNumber" runat="server" Text="Home Phone #"></asp:Label></td>
                            
                            <td align="left" valign="top" style="width: 225px; height: 32px;" >
                                <asp:TextBox ID="TXT_PATIENT_HOME_PHONE" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_PATIENT_HOME_PHONE1" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" style="width: 131px; height: 32px;">
                                <asp:Label ID="lbl_PatientCellPhone" runat="server" Text="Cell  Phone #" Width="124px" CssClass="lbl"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px; height: 32px;">
                                </td>
                            <td align="left" valign="top" style="width: 232px; height: 32px;">
                                <asp:TextBox ID="TXT_PATIENT_CELL_PHONE" runat="server" Width="152px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" >
                                <asp:Label ID="lbl_DateofBirth" runat="server" Text="Date of Birth"></asp:Label></td>
                            
                            <td align="left" valign="top" style="width: 225px; height: 22px;" >
                                <asp:TextBox ID="TXT_PATIENT_DATE_OF_BIRTH" runat="server" Width="2px" Visible="False"></asp:TextBox>
                                 <asp:Label ID="TXT_PATIENT_DATE_OF_BIRTH1" runat="server" Width="2px"></asp:Label>
                            </td>
                            <td align="left" valign="middle" style="width: 131px; height: 22px">
                                <asp:Label ID="lbl_Age" runat="server" Text="Age"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px; height: 22px;">
                                </td>
                            <td align="left" valign="top" style="width: 232px; height: 22px;">
                                <asp:TextBox ID="TXT_PATIENT_AGE" runat="server" Width="17px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_PATIENT_AGE1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" >
                                <asp:Label ID="lbl_Sex" runat="server" Text="Sex"></asp:Label></td>
                            
                            
                                  <td style="width: 225px;" align="left" valign="top">
                  
                                                <asp:RadioButtonList ID="rdblstPATIENT_MALE" runat="server" RepeatDirection="Horizontal" Width="142px" RepeatLayout="Flow" TextAlign="Left" OnSelectedIndexChanged="rdblstPATIENT_MALE_SelectedIndexChanged" Enabled="False" Font-Bold="True" Font-Size="Medium">
                                                    <asp:ListItem Value="0" Text="Male"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Female"></asp:ListItem>
                                                   
                                                </asp:RadioButtonList>
                               
                                                <asp:TextBox ID="txtrdblstPATIENT_MALE" runat="server" Visible="false" Width="45px">-1</asp:TextBox>&nbsp;
                                            </td>
                            <td align="left" valign="top" style="width: 131px">
                                </td>
                            <td align="center" valign="middle" style="width: 9px">
                                </td>
                            <td align="left" valign="top" style="width: 232px">
                               
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="lbl_SOCNumber" runat="server" Text="SOC.SEC.#" CssClass="lbl" Width="115px"></asp:Label>
                            </td>
                            <td align="left" valign="top" style="width: 232px">
                                 <asp:TextBox ID="TXT_PATIENT_SOC_SEC" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_PATIENT_SOC_SEC1" runat="server" Width="1px" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" >
                                <asp:Label ID="lbl_MeritalStatus" runat="server" Text="Marital Status"></asp:Label></td>
                            
                           
                                 <td style="height: 26px; width: 225px;" align="left" class="lbl" valign="top">
                  
                                                <asp:RadioButtonList ID="rdblstPATIENT_SINGLE" runat="server" RepeatDirection="Horizontal" CssClass="lbl" RepeatLayout="Flow" OnSelectedIndexChanged="rdblstPATIENT_SINGLE_SelectedIndexChanged">
                                                    <asp:ListItem Value="3" Text="S"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="M"></asp:ListItem>
                                                     <asp:ListItem Value="1" Text="D"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="W"></asp:ListItem>
                                                   
                                                </asp:RadioButtonList>
                               
                                                <asp:TextBox ID="txtrdblstPATIENT_SINGLE" runat="server" Visible="false" Width="84px">-1</asp:TextBox>
                                            </td>
                            <td align="left" valign="top" style="width: 131px; height: 26px;">
                                </td>
                            <td align="center" valign="middle" style="width: 9px; height: 26px;">
                                </td>
                            <td align="left" valign="top" style="width: 232px; height: 26px;">
                                </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="lbl_SpouseName" runat="server" Text="Spouse's Name" Width="111px"></asp:Label>
                            </td>
                            <td align="left" valign="top" style="width: 232px; height: 26px;">
                                <asp:TextBox ID="TXT_SPOUSE_NAME" runat="server" Width="218px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" >
                                <asp:Label ID="lbl_SpouseSOCNumber" runat="server" Text="Spouse SOC.SEC.#"></asp:Label></td>
                            
                            <td align="left" valign="middle" colspan="4" style="height: 22px">
                                <asp:TextBox ID="TXT_SPOUSE_SOC_SEC" runat="server" Width="222px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="height: 20px" >
                                <asp:Label ID="lbl_BillResponse" runat="server" Text="Responsible for Your Bill" Width="163px"></asp:Label></td>
                            
                            <td align="left" colspan="4" valign="middle" class="lbl" style="height: 20px">
                                <asp:CheckBox ID="CHK_RESPONSIBLE_BILL_SELF" runat="server" Text="Self" />
                                <asp:CheckBox ID="CHK_RESPONSIBLE_BILL_AUTO_INS" runat="server" Text="Auto Ins." />
                                <asp:CheckBox ID="CHK_RESPONSIBLE_BILL_WORKERS_COMP" runat="server" Text="Worker's Comp" />
                                <asp:CheckBox ID="CHK_RESPONSIBLE_BILL_SELF_PERSONAL_HEALTH_INS" runat="server" Text=" Personal Health Ins" />
                                <asp:CheckBox ID="CHK_RESPONSIBLE_BILL_MEDICARE" runat="server" Text="Medicare" /></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" >
                                <asp:Label ID="lbl_Other" runat="server" Text="Other" Width="101px"></asp:Label></td>
                            
                            <td align="left" valign="middle" colspan="4">
                                <asp:TextBox ID="TXT_RESPONSIBLE_BILL_OTHER" runat="server" Height="37px" TextMode="MultiLine"
                                    Width="226px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" colspan="6">
                                <asp:Label ID="lbl_EmployerInformation" runat="server" Style="font-weight: bold"
                                    Text="EMPLOYER INFORMATION"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 214px" >
                                <asp:Label ID="lbl_Occupation" runat="server" Text="Occupation"></asp:Label></td>
                           
                            <td align="left" valign="middle" style="width: 225px" >
                                <asp:TextBox ID="TXT_OCCUPATION" runat="server" Width="255px"></asp:TextBox></td>
                            <td align="left" valign="middle" style="width: 131px">
                                <asp:Label ID="lbl_Employer" runat="server" Text="Employer" Width="63px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                </td>
                            <td align="left" valign="top" style="width: 232px">
                                <asp:TextBox ID="TXT_EMPLOYER" runat="server" Width="0px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_EMPLOYER1" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="height: 6px; width: 214px;" >
                                <asp:Label ID="lbl_EmployerAddress" runat="server" Text="Address"></asp:Label></td>
                            
                            <td align="left" valign="top" style="width: 225px; height: 6px;" >
                                <asp:TextBox ID="TXT_ADDRESS" runat="server" Width="0px" Visible="False" ></asp:TextBox>
                                <asp:Label ID="TXT_ADDRESS1" runat="server" ></asp:Label>
                            </td>
                            <td align="left" valign="top" style="height: 6px; width: 131px;">
                                <asp:Label ID="lbl_EmployerCity" runat="server" Text="City"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px; height: 6px;">
                                </td>
                            <td align="left" valign="top" style="width: 232px; height: 6px;">
                                <asp:TextBox ID="TXT_CITY" runat="server" Width="0px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_CITY1" runat="server" ></asp:Label>
                           </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="height: 22px; width: 214px;" >
                                <asp:Label ID="lbl_EmployerState" runat="server" Text="State"></asp:Label></td>
                            
                            <td align="left" valign="top" style="width: 225px; height: 22px;" >
                                <asp:TextBox ID="TXT_STATE" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_STATE1" runat="server"></asp:Label>
                                
                            </td>
                            <td align="left" valign="middle" style="width: 131px; height: 22px">
                                <asp:Label ID="lbl_EmployerZip" runat="server" Text="Zip Code" Width="120px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px; height: 22px;">
                                </td>
                            <td align="left" valign="top" style="width: 232px; height: 22px;">
                                <asp:TextBox ID="TXT_ZIP" runat="server" Width="0px" Visible="False"></asp:TextBox>
                               <asp:Label ID="TXT_ZIP1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="height: 22px; width: 214px;" >
                                <asp:Label ID="lbl_EmployerBusinessPhoneNo" runat="server" Text="Business Phone #"></asp:Label></td>
                           
                            <td align="left" valign="top" style="width: 225px; height: 22px;" >
                                <asp:TextBox ID="TXT_BUSINESS_PHONE_EXTENSION" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_BUSINESS_PHONE" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_BUSINESS_PHONE1" runat="server"></asp:Label> 
                            </td>
                            <td align="left" valign="top" style="width: 131px; height: 22px">
                                <asp:Label ID="lbl_EmployerFaxNumber" runat="server" Text="Fax #"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px; height: 22px;">
                                </td>
                            <td align="left" valign="top" style="width: 232px; height: 22px;">
                                <asp:TextBox ID="TXT_FAX_EXTENSION" runat="server" Width="40px"></asp:TextBox>
                                <asp:TextBox ID="TXT_FAX" runat="server" Width="104px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" colspan="6">
                                <asp:Label ID="lbl_InjuryInformation" runat="server" Style="font-weight: bold" Text="INJURY INFORMATION"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 214px" >
                                <asp:Label ID="lblInsuranceType" runat="server" Text="Insurance Type"></asp:Label></td>
                            
                            <td align="left" valign="middle" colspan="4" class="lbl">
                                <asp:CheckBox ID="CHK_INSURANCE_TYPE_PRIVATE" runat="server" Text="Private" />
                                <asp:CheckBox ID="CHK_INSURANCE_TYPE_AUTO" runat="server" Text="Auto" />
                                <asp:CheckBox ID="CHK_INSURANCE_TYPE_WORK" runat="server" Text="Work" />
                                <asp:CheckBox ID="CHK_INSURANCE_TYPE_SLIP_FALL" runat="server" Text="Slip & Fall" />
                                <asp:CheckBox ID="CHK_INSURANCE_TYPE_OTHER" runat="server" Text="Other" />
                                </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 214px">
                                <asp:Label ID="lbl_OtherInsuranceType" runat="server" Text="Other"></asp:Label></td>
                           
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_INSURANCE_TYPE_OTHER" runat="server" Width="420px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 214px" >
                                <asp:Label ID="lbl_PatientRelationship" runat="server" Text="Patient Relationship to Insured" Width="181px"></asp:Label></td>
                            
                            <td align="left" valign="middle" colspan="4" class="lbl">
                             <asp:RadioButtonList ID="rdblstPATIENT_RELATIONSHIP_TO_INSURED" runat="server" RepeatDirection="Horizontal" CssClass="lbl" RepeatLayout="Flow" OnSelectedIndexChanged="rdblstPATIENT_RELATIONSHIP_TO_INSURED_SelectedIndexChanged">
                                                    <asp:ListItem Value="4" Text="Self"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="Spouse"></asp:ListItem>
                                                     <asp:ListItem Value="1" Text="Child"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Driver"></asp:ListItem>
                                                   
                                                </asp:RadioButtonList>
                               
                                                <asp:TextBox ID="txtrdblstPATIENT_RELATIONSHIP_TO_INSURED" runat="server" Visible="false" Width="90px">-1</asp:TextBox>
                                
                                </td>
                                
                                
                                
                                
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 214px">
                                <asp:Label ID="lbl_OtherPatientRelationshipInsured" runat="server" Text="Other"></asp:Label></td>
                            
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_PATIENT_RELATIONSHIP_TO_INSURED_OTHER" runat="server" Width="455px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 214px">
                                <asp:Label ID="lbl_DateofInjury" runat="server" Text="Date of Injury"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 5px">
                                <asp:TextBox ID="TXT_DATE_OF_INJURY" runat="server" Width="14px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_DATE_OF_INJURY1" runat="server" Width="14px"></asp:Label></td>
                            <td align="left" colspan="4" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 214px" >
                                <asp:Label ID="lbl_AutoInjury" runat="server" Text="If auto injury, where you?"></asp:Label></td>
                            
                            <td align="left" valign="middle" colspan="4">
                                <asp:RadioButtonList ID="rdblstAUTO_INJURY_ROLE" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rdblstAUTO_INJURY_ROLE_SelectedIndexChanged" Enabled="False" Font-Bold="True" Font-Size="Medium">
                                                    <asp:ListItem Value="0" Text="Driver"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Passanger"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Pedestrian"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Other"></asp:ListItem>
                                                   
                                                </asp:RadioButtonList>
                               
                                                <asp:TextBox ID="txtrdblstAUTO_INJURY_ROLE" runat="server" Visible="false" Width="87px">-1</asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="height: 22px; width: 214px;">
                                <asp:Label ID="lbl_OtherAutoInjury" runat="server" Text="Other"></asp:Label></td>
                            
                            <td align="left" colspan="4" valign="middle" style="height: 22px">
                                <asp:TextBox ID="TXT_AUTO_INJURY_OTHER" runat="server" Width="262px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 214px" >
                                <asp:Label ID="lbl_PolicyNumber" runat="server" Text="Policy #"></asp:Label></td>
                            
                            <td align="left" valign="top" style="width: 225px" >
                                <asp:TextBox ID="TXT_POLICY_NUMBER" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_POLICY_NUMBER1" runat="server" ></asp:Label>
                            </td>
                            <td align="left" valign="middle" style="width: 131px">
                                <asp:Label ID="lbl_Claimnumber" runat="server" Text="Claim #"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                </td>
                            <td align="left" valign="top" style="width: 232px">
                                <asp:TextBox ID="TXT_CLAIM_NUMBER" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_CLAIM_NUMBER1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 214px" >
                                <asp:Label ID="lbl_WorkersCompNumber" runat="server" Text="Workers Comp #"></asp:Label>
                            </td>
                            
                            <td align="left" valign="middle" colspan="4">
                                <asp:TextBox ID="TXT_WORKERS_COMP" runat="server" Width="302px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top" style="width: 214px">
                                <asp:Label ID="lbl_Injuryhappened" runat="server" Text="Describe how injury happened?"></asp:Label></td>
                            
                            <td align="left" colspan="4" valign="top">
                                <asp:TextBox ID="TXT_HOW_INJURY_HAPPEN" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                 <asp:Label ID="TXT_HOW_INJURY_HAPPEN1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 214px" >
                                <asp:Label ID="lblReportInjury" runat="server" Text="Did you report Injury?" Width="186px"></asp:Label></td>
                            
                            <td align="left" valign="middle" style="width: 225px" >
                                <asp:RadioButtonList ID="rdblstREPORT_INJURY_YES_NO" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rdblstREPORT_INJURY_YES_NO_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                                   
                                                </asp:RadioButtonList>
                               
                                                <asp:TextBox ID="txtrdblstREPORT_INJURY_YES_NO" runat="server" Visible="false" Width="96px">-1</asp:TextBox></td>
                            <td align="left" valign="middle" style="width: 100%">
                                <asp:Label ID="lbl_ReportInjuryToWhom" runat="server" Text="To Whom?" Width="100px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                </td>
                            <td align="left" valign="middle" style="width: 232px">
                                <asp:TextBox ID="TXT_REPORT_INJURY_TO_WHOM" runat="server" Width="152px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 214px" >
                                <asp:Label ID="lbl_wereyouhospitalized" runat="server" Text="were you Hospitalized?" Width="189px"></asp:Label></td>
                            
                            <td align="left" valign="middle" style="width: 225px" >
                                 <asp:RadioButtonList ID="rdblstHOSPITALIZED_YES_NO" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rdblstHOSPITALIZED_YES_NO_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                                   
                                                </asp:RadioButtonList>
                               
                                                <asp:TextBox ID="txtrdblstHOSPITALIZED_YES_NO" runat="server" Visible="false" Width="95px">-1</asp:TextBox></td>
                            <td align="left" valign="middle" style="width: 100%">
                                <asp:Label ID="lbl_where" runat="server" Text="Where?"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                </td>
                            <td align="left" valign="middle" style="width: 232px">
                                <asp:TextBox ID="TXT_HOSPITALIZED_WHERE" runat="server" Width="152px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 214px" >
                                <asp:Label ID="lbl_Xrays_taken" runat="server" Text=" X_Rays Taken?" Width="169px"></asp:Label></td>
                            
                            <td align="left" valign="middle" style="width: 225px">
                            <asp:RadioButtonList ID="rdblstX_RAYS_YES_NO" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rdblstX_RAYS_YES_NO_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                                   
                                                </asp:RadioButtonList>
                               
                                                <asp:TextBox ID="txtrdblstX_RAYS_YES_NO" runat="server" Visible="false" Width="95px">-1</asp:TextBox></td>
                                
                            <td align="left" valign="middle" style="width: 100%">
                                <asp:Label ID="lblByWhom" runat="server" Text="By Whom?" Width="122px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                </td>
                            <td align="left" valign="middle" style="width: 232px">
                                <asp:TextBox ID="TXT_X_RAYS_BY_WHOM" runat="server" Width="152px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 214px">
                                <asp:Label ID="lbl_wereyouworking" runat="server" Text="were you working at the time of the Accident?" Width="196px"></asp:Label></td>
                            
                            <td align="left" valign="middle" style="width: 225px">
                                 <asp:RadioButtonList ID="rdblstWERE_YOU_WORKING" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rdblstWERE_YOU_WORKING_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                                   
                                                </asp:RadioButtonList>
                               
                                                <asp:TextBox ID="txtrdblstWERE_YOU_WORKING" runat="server" Visible="false" Width="95px">-1</asp:TextBox></td>
                            <td align="left" valign="top" style="width: 100%">
                                <asp:Label ID="lbl_DatelostFromWork" runat="server" Text="Dates  lost from Work" Width="130px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                </td>
                            <td align="left" valign="middle" style="width: 232px">
                                <asp:TextBox ID="TXT_DATES_LOST_FROM_WORK" runat="server" Width="112px"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnATAccidentDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                <ajaxToolkit:CalendarExtender ID="calATAccidentDate" runat="server" TargetControlID="TXT_DATES_LOST_FROM_WORK"
                                                                                    PopupButtonID="imgbtnATAccidentDate" Enabled="True" />
                                </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 214px">
                                <asp:Label ID="lbl_OtherDoctors" runat="server" Text="Name of Other Doctors seen FRO this Injury? " Width="198px"></asp:Label></td>
                           
                            <td align="left" valign="middle" style="width: 225px">
                                <asp:TextBox ID="TXT_OTHER_DOCTORS_NAME" runat="server" Width="302px"></asp:TextBox></td>
                            <td align="left" valign="top" style="width: 100%">
                                <asp:Label ID="lbl_NameofAttorny" runat="server" Text="Name of Attorney" Width="125px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                </td>
                            <td align="left" valign="top" style="width: 100%">
                                <asp:TextBox ID="TXT_ATTORNEY_NAME" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_ATTORNEY_NAME1" runat="server" Width="1px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 214px">
                                <asp:Label ID="lbl_AttorneyPhoneNumber" runat="server" Text="Attorney's Phone #" Width="196px"></asp:Label></td>
                            
                            <td align="left" colspan="4" valign="top">
                                <asp:TextBox ID="TXT_ATTORNEY_PHONE" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_ATTORNEY_PHONE1" runat="server" Width="1px"></asp:Label>    
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="TDPart" valign="top">
                <table style="width: 100%">
                    <tr>
                        <td align="center" style="height: 26px">
                <asp:TextBox ID="TXT_I_EVENT" runat="server" Visible="false">1</asp:TextBox>
                           <asp:Button ID="css_btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="css_btnSave_Click" />
                            <asp:TextBox ID="TXT_CASE_ID" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label  ID="TXT_CASE_ID1" runat="server" Width="1px" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
                </td>
        </tr>
    </table>

</asp:Content>

