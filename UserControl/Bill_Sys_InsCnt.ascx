<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_InsCnt.ascx.cs"
    Inherits="UserControl_Bill_Sys_InsCnt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<script language="javascript" type="text/javascript">
    function GetInsuranceValue(source, eventArgs)
    {
        document.getElementById("<%=hdninsurancecode.ClientID %>").value = eventArgs.get_value();
    }
</script>

<%--<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>--%>
<table >
    <tr>
        <td>
            Ins. Name
        </td>
        <td style="width: 60%" align="left" colspan="3">
            <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoIns" EnableCaching="true"
                DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtInsuranceCompany"
                ServiceMethod="GetInsurance" ServicePath="../AJAX Pages/PatientService.asmx"
                UseContextKey="true" ContextKey="SZ_COMPANY_ID" OnClientItemSelected="GetInsuranceValue">
            </ajaxToolkit:AutoCompleteExtender>
            <asp:TextBox ID="txtInsuranceCompany" runat="Server" autocomplete="off" Width="80%"
                AutoPostBack="true"  OnTextChanged="txtInsuranceCompany_TextChanged" />
           
            <extddl:ExtendedDropDownList ID="extddlInsuranceCompany" Width="96%" runat="server"
                Visible="false" Connection_Key="Connection_String" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                OldText="" StausText="False"  OnextendDropDown_SelectedIndexChanged="extddlInsuranceCompany_extendDropDown_SelectedIndexChanged" />
            <%-- --%>
        </td>
    </tr>
    <tr>
        <td>
            Ins. Address
        </td>
        <td style="width: 60%" align="left" colspan="3">
            <asp:ListBox Width="80%" ID="lstInsuranceCompanyAddress" AutoPostBack="true" runat="server" OnSelectedIndexChanged="lstInsuranceCompanyAddress_SelectedIndexChanged">
            </asp:ListBox>
            <%--  --%>
        </td>
        
    </tr>
    <tr>
        <td style="width: 18%">
            Address
        </td>
           <td style="width: 30%">
           <asp:TextBox Width="80%" ID="txtInsuranceAddress" runat="server" CssClass="text-box"
                    ReadOnly="True"></asp:TextBox>
          </td>
             <td style="width: 18%">
            Street
        </td>
           <td style="width: 30%">
           <asp:TextBox Width="80%" ID="txtInsuranceStreet" runat="server" CssClass="text-box"
                    ReadOnly="True"></asp:TextBox>
          </td>
        
    </tr>
    <tr>
     
          <td style="width: 18%">
             
                    City
            </td>
            <td style="width: 30%">
                <asp:TextBox ID="txtInsuranceCity" runat="server" CssClass="text-box" ReadOnly="True" Width="80%"></asp:TextBox>
            </td>
           <td style="width: 18%">
                
                   State
            </td>
             <td style="width: 30%">
                <asp:TextBox ID="txtInsuranceState" runat="server" CssClass="text-box" ReadOnly="True"
                    Width="80%"></asp:TextBox>
            </td>
      
    </tr>
    
    <tr>
     
          <td style="width: 18%">
             
                  Zip
            </td>
            <td style="width: 30%">
                <asp:TextBox Width="80%" ID="txtInsuranceZip" runat="server" CssClass="text-box"
                    ReadOnly="True"></asp:TextBox>
            </td>
           <td style="width: 18%">
                
                     Phone
            </td>
             <td style="width: 30%">
                <asp:TextBox ID="txtInsPhone" runat="server" CssClass="text-box" ReadOnly="True"
                    Width="80%"></asp:TextBox>
            </td>
      
    </tr>
    
    <tr>
     
          <td style="width: 18%">
             
                        Fax
            </td>
            <td style="width: 30%">
                 <asp:TextBox ID="txtInsFax" runat="server" CssClass="text-box" ReadOnly="True" Width="80%"></asp:TextBox>
            </td>
           <td style="width: 18%">
                
                     Policy #
            </td>
             <td style="width: 30%">
                
                    <asp:TextBox ID="txtPolicyNumber" runat="server" CssClass="text-box" Width="80%"></asp:TextBox>
            </td>
      
    </tr>
    
       <tr>
     
          <td style="width: 18%">
             
                      Claim No
            </td>
            <td style="width: 30%">
                   <asp:TextBox ID="txtClaimNumber" runat="server" CssClass="text-box" Width="80%"></asp:TextBox>
            </td>
           <td style="width: 18%">
                
                    Policy Holder
            </td>
             <td style="width: 30%">
                
                     <asp:TextBox ID="txtPolicyHolder" runat="server" CssClass="text-box" Width="80%"></asp:TextBox>
            </td>
      
    </tr>
    
      <tr>
     
          <td style="width: 18%">
             
                          Case Type
            </td>
            <td style="width: 30%">
                     <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="80%" Selected_Text="---Select---"
                    Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String" Enabled="false">
                </extddl:ExtendedDropDownList>
            </td>
           <td style="width: 18%">
                
                      Date of Accident
            </td>
             <td style="width: 30%">
                
                    <asp:TextBox Width="80%" ID="txtdateofaccident" runat="server" MaxLength="10" CssClass="textbox"></asp:TextBox>&nbsp;
                <asp:ImageButton ID="imgbtnATAccidentDate" runat="server" ImageUrl="~/Images/cal.gif" />
                <ajaxToolkit:CalendarExtender ID="calATAccidentDate" runat="server" TargetControlID="txtdateofaccident"
                    PopupButtonID="imgbtnATAccidentDate" Enabled="True" PopupPosition="TopLeft" />
            </td>
      
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:HiddenField ID="hdninsurancecode" runat="server" />
            <asp:TextBox ID="txtPatientId" runat="server" Visible="false"></asp:TextBox>
              <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox>
              <asp:TextBox ID="txtEventID" runat="server" Visible="false"></asp:TextBox>
        </td>
    </tr>
</table>
