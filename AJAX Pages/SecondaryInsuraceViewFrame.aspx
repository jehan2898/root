<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecondaryInsuraceViewFrame.aspx.cs"
    Inherits="AJAX_Pages_SecondaryInsuraceViewFrame" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx"TagName="MessageControl"
    TagPrefix="UserMessage" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href ="Css/main-ie.css"rel ="Stylesheet" type ="text/css" />
    <style type="text/css">
        .style2
        {
            width: 35px;
        }
        .style3
        {
            width: 98px;
        }
        .style4
        {
            width: 262px;
        }
    </style>
</head>

    <script language="javascript" type="text/javascript">
        function GetInsuranceValue(source, eventArgs) {
            //alert(eventArgs.get_value());
            document.getElementById("<%=hdninsurancecode.ClientID %>").value = eventArgs.get_value();
        }
    </script>
<body>
    <form id="form1" runat="server">
        <div>
            
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <tr>
                <td colspan ="4" style="height: 10px">
                  <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
                </td><td align="right">
                <asp:Label ID="lblpop" Text="*-mandatory fields" ForeColor="red" runat="server"></asp:Label>
                </td>
                </tr>
               
                <tr >
                <td colspan ="4">
                     <asp:TextBox ID="txtCompanyID" runat="server" Style="visibility: hidden;" Width="10px"></asp:TextBox>
                      <asp:TextBox ID="txtPatientID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                      <asp:Label ID="lblerrormsg" runat="server" ForeColor ="red"  Visible ="false"   Text=""></asp:Label>
                    </td>                    
                </tr>
                   <tr>
                    <td class="tablecellLabel" style="height: 30px">
                        <div class="td-PatientInfo-lf-desc-ch">
                            Name
                        </div>
                    </td>
                    <td class="tablecellSpace" style="height: 30px">
                    </td>
                    
                    <td colspan="2" style="height: 30px">
                           <asp:HiddenField ID="hdninsurancecode" runat="server" />
                          <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoIns" EnableCaching="true"
                            DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtInsuranceCompany"
                            ServiceMethod="GetInsurance" ServicePath="PatientService.asmx" UseContextKey="true"
                            ContextKey="SZ_COMPANY_ID" OnClientItemSelected="GetInsuranceValue">
                        </ajaxToolkit:AutoCompleteExtender>
                        <asp:TextBox ID="txtInsuranceCompany" runat="Server" autocomplete="off" Width="75%"
                            OnTextChanged="txtInsuranceCompany_TextChanged" AutoPostBack="true" />
                        <extddl:ExtendedDropDownList ID="extddlInsuranceCompany" Width="96%" runat="server"
                            Visible="false" Connection_Key="Connection_String" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                            Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                            OnextendDropDown_SelectedIndexChanged="extddlInsuranceCompany_extendDropDown_SelectedIndexChanged"
                            OldText="" StausText="False" />                 
                       <%-- <a href="#" id="A1" onclick="showAdjusterPanelAddress()" style="text-decoration: none;">
                            <img id="img1" src="Images/actionEdit.gif" style="border-style: none;" title="Add Insurance Company Address"  /></a>--%>
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        <div class="td-PatientInfo-lf-desc-ch">
                            Ins. Address
                        </div>
                    </td>
                    <td class="tablecellSpace">
                        &nbsp;</td>
                    <td colspan="2">
                        <asp:ListBox Width="100%" ID="lstInsuranceCompanyAddress" AutoPostBack="true" runat="server"
                            OnSelectedIndexChanged="lstInsuranceCompanyAddress_SelectedIndexChanged"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        <div class="td-PatientInfo-lf-desc-ch">
                            Address</div>
                    </td>
                    <td class="tablecellSpace">
                    </td>
                    <td colspan="2">
                        <asp:TextBox Width="99%" ID="txtInsuranceAddress" runat="server" CssClass="text-box"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        <div class="td-PatientInfo-lf-desc-ch">
                            City</div>
                    </td>
                    <td class="tablecellSpace">
                    </td>
                    <td colspan="2">
                        <div class="lbl">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="25%">
                                        <asp:TextBox ID="txtInsuranceCity" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                    <td width="13%" class="tablecellLabel">
                                        <div class="td-PatientInfo-lf-desc-ch" style ="width:30px">
                                            State</div>
                                    </td>
                                    <td width="25%">
                                        <asp:TextBox ID="txtInsuranceState" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                    <td width="15%" class="tablecellLabel">
                                        <div class="td-PatientInfo-lf-desc-ch">
                                            Zip</div>
                                    </td>
                                    <td width="25%">
                                        <asp:TextBox Width="80%" ID="txtInsuranceZip" runat="server" CssClass="text-box"
                                            ReadOnly="True"></asp:TextBox></td>
                                </tr>
                             </table>
                          </div>
                      </td>
                  </tr>
                  <tr>
                    <td class="tablecellLabel">
                        <div class="td-PatientInfo-lf-desc-ch">
                            Phone</div>
                    </td>
                    <td class="tablecellSpace">
                    </td>
                    <td colspan="2">
                        <div class="lbl">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="25%">
                                        <asp:TextBox ID="txtInsPhone" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                    <td width="13%" class="tablecellLabel">
                                        <div class="td-PatientInfo-lf-desc-ch" style ="width:30px">
                                            Fax</div>
                                    </td>
                                    <td width="25%">
                                        <asp:TextBox ID="txtInsFax" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                    <td width="15%" class="tablecellLabel">
                                        <div class="td-PatientInfo-lf-desc-ch">
                                            Contact Person</div>
                                    </td>
                                    <td width="25%">
                                        <asp:TextBox Width="80%" ID="txtInsContactPerson" runat="server" CssClass="text-box"
                                            ReadOnly="True"></asp:TextBox></td>
                                </tr>
                                
                                
                            </table>
                            <%-- <table>
                                                                                    <tr>
                                                                                       <td class="td-PatientInfo-lf-desc-ch">
                                                                                               Carrier Code #
                                                                                               </td>
                                                                                               <td>
                                                                                               <asp:TextBox ID="txtcarriercode" runat="server" ></asp:TextBox>
                                                                                               
                                                                                               </td>
                                                                                      </tr>
                                                                                    </table>--%>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td height="26" class="tablecellLabel">
                        
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        <div class="td-PatientInfo-lf-desc-ch">
                            Insurance Type</div>
                    </td>
                    <td class="tablecellSpace">
                    </td>
                    <td colspan="2" class="tablecellLabel">
                        <asp:DropDownList ID="ddlInsuranceType" runat="server" OnSelectedIndexChanged="ddlInsuranceType_SelectedIndexChanged"> 
                         <asp:ListItem >Select</asp:ListItem>
                        <asp:ListItem >Secondary</asp:ListItem>
                         <asp:ListItem >Major Medical</asp:ListItem>
                          <asp:ListItem >Private</asp:ListItem>
                          <asp:ListItem >WC</asp:ListItem>
                          <asp:ListItem >Auto</asp:ListItem>
                        </asp:DropDownList><asp:Label ID="Label1" runat="server" ForeColor="red" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        &nbsp;</td>
                    <td class="tablecellSpace">
                        &nbsp;</td>
                    <td colspan="2" class="tablecellLabel">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="tablecellLabel" colspan="4">
                    
                        <table>
                        <tr>
                        <td class="tablecellLabel"  style="width:150px">
                                    <div class="td-PatientInfo-lf-desc-ch" style="width:150px">Relation To Patient</div>
                                </td>
                                <td colspan="5">
                                    <asp:RadioButtonList id="rdlPolicyHolderRelation" runat="server" 
                                        RepeatDirection="Horizontal" AutoPostBack="True" 
                                        onselectedindexchanged="rdlPolicyHolderRelation_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Text="Self" ></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Spous"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Child"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Other"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                </tr>
                            <tr>
                                <td class="tablecellLabel" style="width:150px">
                                    <div class="td-PatientInfo-lf-desc-ch">Policy Holder</div>
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtPolicyHolder" runat="server" Width="75%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tablecellLabel">
                                    <div class="td-PatientInfo-lf-desc-ch">Policy Holder Address</div>
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtPolicyHolderAddr" runat="server" Width="75%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td  class="tablecellLabel">
                                    <div class="td-PatientInfo-lf-desc-ch">Policy #</div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSecondaryPolicyNumber" runat="server" CssClass="text-box" ></asp:TextBox>
                                </td>
                                <td class="tablecellLabel">
                                    <div class="td-PatientInfo-lf-desc-ch">Claim #</div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSecondaryClaimNumber" runat="server" CssClass="text-box" ></asp:TextBox>
                                </td>
                              </tr>
                            <tr>
                                <td  class="tablecellLabel">
                                    <div class="td-PatientInfo-lf-desc-ch">City</div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPolicyCity" runat="server" CssClass="text-box" ></asp:TextBox>
                                </td>
                                <td class="tablecellLabel">
                                    <div class="td-PatientInfo-lf-desc-ch">State</div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPolicyState" runat="server" CssClass="text-box" visible="False" ></asp:TextBox>
                                    <extddl:ExtendedDropDownList ID="extdlPolicyState" runat="server" Width="90%" Connection_Key="Connection_String"
                                        Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---"
                                        OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                </td>
                                <td class="tablecellLabel" style="width:10px">
                                    <div class="td-PatientInfo-lf-desc-ch" style="width:30px">Zip</div>
                                </td>
                                <td>
                                    <asp:TextBox Width="80%" ID="txtPolicyZip" runat="server" CssClass="text-box"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tablecellLabel">
                                    <div class="td-PatientInfo-lf-desc-ch">Phone Number</div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPolicyPhone" runat="server" CssClass="text-box"></asp:TextBox>
                                </td>
                                <%--<td class="tablecellLabel">
                                    <div class="td-PatientInfo-lf-desc-ch" style="width:150px">Relation To Patient</div>
                                </td>
                                <td colspan="3">
                                    <asp:RadioButtonList id="rdlPolicyHolderRelation" runat="server" 
                                        RepeatDirection="Horizontal" AutoPostBack="True" 
                                        onselectedindexchanged="rdlPolicyHolderRelation_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Text="Self" ></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Spous"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Child"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Other"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>--%>
                            </tr>

                        </table>
                    
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel" colspan="4">
                        &nbsp;</td>
                </tr>
                <tr>
                <td colspan ="4">
                <table width="100%">
                <tr>
                 <td  class="ContentLabel" align ="right"  colspan="2">
                        <dx:ASPxButton ID="btnsave" runat="server" Text="Add" Width="80px" OnClick="btnsave_Click"  ></dx:ASPxButton>                 
                    </td>
                     <td class="ContentLabel" align ="center" colspan="2" >
                      <dx:ASPxButton ID="btnupdate" runat="server" Text="Update" Width="80px" OnClick="btnUpdate_Click" ></dx:ASPxButton>   
                    </td>
                    <td  class="ContentLabel" align="left" colspan="2">                       
                        <dx:ASPxButton id="btndelete"  runat="server" Text="Delete" Width="80px" OnClick="btnDelete_Click" Visible="false" ></dx:ASPxButton>                         
                    </td>
                </tr>
                </table>
                </td>
                     
                </tr>
                
            </table>
            <asp:HiddenField ID="secondaryInsuranceID" runat="server" />
            <asp:HiddenField ID="Link" runat="server" />
        </div>
    </form>
</body>
</html>
