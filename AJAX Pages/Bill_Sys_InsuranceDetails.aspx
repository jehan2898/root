<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_InsuranceDetails.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_InsuranceDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
    
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <link href ="Css/main-ie.css"rel ="Stylesheet" type ="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptmanager1" runat="server">
    </asp:ScriptManager>
    <div>
    
        <table class="style1">
        <tr align="center"><td><asp:UpdatePanel ID="UpdatePanelUserMessage" runat="server">
                                <ContentTemplate>
                                <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                </ContentTemplate>
                                </asp:UpdatePanel></td></tr>
        
        
        <tr>
            <td>
                <div style="border:2px solid">
                <table class="style1">
                    <tr>
                        <td align="center" class="tablecellLabel" style="background-color: #DCDCDC" 
                            width="100%">
                            <asp:Label ID="Label20" 
                runat="server" Font-Bold="True" 
                                    Text="PRIMARY INSURANCE DETAILS" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tablecellLabel" width="100%">
            <table>
                                           
                        
                        <tr>
                            <td class="tablecellLabel">
                                <div class="td-PatientInfo-lf-desc-ch">
                            Insurance Name</div></td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="txtPriInsName" runat="server" Width="495px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">
                            <div class="td-PatientInfo-lf-desc-ch">
                            Insurance City</div>                                
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPriInsCity" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tablecellLabel">
                                <div class="td-PatientInfo-lf-desc-ch" style="text-align:right">
                            Insurance Zip</div></td>
                            <td align="left">
                                <asp:TextBox ID="txtPriInsZip" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">
                            <div class="td-PatientInfo-lf-desc-ch">
                            Insurance Address</div>                                
                            </td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="txtPriInsAddress" runat="server" TextMode="MultiLine" 
                                    Width="495px" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">                             
                                <div class="td-PatientInfo-lf-desc-ch">
                            State</div>
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtPriInsStateExtd" runat="server" CssClass="text-box" 
                                    visible="False" ReadOnly="True" ></asp:TextBox>
                                    <extddl:ExtendedDropDownList ID="extdlPriInsState" runat="server" 
                                    Width="90%" Connection_Key="Connection_String"
                                        Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---"
                                        OldText="" StausText="False" Enabled="False"></extddl:ExtendedDropDownList>
                            </td>
                        </tr>
                        
                    </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tablecellLabel" style="background-color: #DCDCDC" 
                            width="100%">
            <asp:Label ID="Label23" runat="server" Font-Bold="True" 
                                    Text="PRIMARY POLICY HOLDER DETAILS" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tablecellLabel" width="100%">
            <table>     
                        <tr>
                            <td class="tablecellLabel">
                            <div class="td-PatientInfo-lf-desc-ch">
                            Policy No</div>
                                
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPriPolicyNo" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tablecellLabel">
                            </td>
                            <td align="left">
                                
                            </td>
                        </tr>
                                           
                        
                        
                        <tr>
                            <td class="tablecellLabel">
                                <div class="td-PatientInfo-lf-desc-ch">
                            Policy Holder</div></td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtPriPolicyHolder" runat="server" Width="495px" 
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">
                                <div class="td-PatientInfo-lf-desc-ch">
                                    City</div>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPriPolicyCity" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tablecellLabel">
                                <div class="td-PatientInfo-lf-desc-ch" style="text-align:right">
                            Zip</div></td>
                            <td align="left">
                                <asp:TextBox ID="txtPriPolicyZip" runat="server" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">
                                <div class="td-PatientInfo-lf-desc-ch">
                            Address</div></td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtPriPolicyHolderAddr" runat="server" TextMode="MultiLine" 
                                    Width="495px" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">                                                          
                                <div class="td-PatientInfo-lf-desc-ch">
                            Phone No</div></td>
                            <td align="left">
                                <asp:TextBox ID="txtPriPolicyPh" runat="server" ReadOnly="True"></asp:TextBox></td>
                            <td class="tablecellLabel">
                                &nbsp;</td>
                            <td align="left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">
                                 <div class="td-PatientInfo-lf-desc-ch">
                            State</div>                         
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtPriPolicyState" runat="server" CssClass="text-box" 
                                    visible="False" ReadOnly="True" ></asp:TextBox>
                                    <extddl:ExtendedDropDownList ID="extdlPriPolicyState" runat="server" 
                                    Width="90%" Connection_Key="Connection_String"
                                        Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---"
                                        OldText="" StausText="False" Enabled="False"></extddl:ExtendedDropDownList></td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">
                                <div class="td-PatientInfo-lf-desc-ch">
                            Relation with Patient</div></td>
                            <td align="left" colspan="3">
                                <asp:RadioButtonList id="rdlPriPolicyRelation" runat="server" 
                                        RepeatDirection="Horizontal" Enabled="False" >
                                    <asp:ListItem Value="1" Text="Self" ></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Spous"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Child"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Other"></asp:ListItem>
                                    </asp:RadioButtonList></td>
                        </tr>
                                                                        
                    </table>
                        </td>
                    </tr>
                </table>
                </div> 
            </td>
        </tr>
        
        <tr>
            <td>
                
            </td>
        </tr>


        <tr>
            <td>
                <div style="border:2px solid">
                    
                    <table class="style1" width="100%">
                        <tr>
                            <td align="center" class="tablecellLabel" style="background-color: #DCDCDC">
                                <asp:Label ID="Label24" runat="server" Font-Bold="True" 
                                    Text="SECONDARY INSURANCE DETAILS" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tablecellLabel">
            <table>
                                           
                        
                        
                        <tr>
                            <td class="tablecellLabel">
                                <div class="td-PatientInfo-lf-desc-ch">
                            Insurance Name</div></td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="txtSecInsName" runat="server" Width="495px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">
                                <div class="td-PatientInfo-lf-desc-ch">
                            Insurance City</div></td>
                            <td align="left">
                                <asp:TextBox ID="txtSecInsCity" runat="server" ReadOnly="True"></asp:TextBox></td>
                            <td class="tablecellLabel">
                                <div class="td-PatientInfo-lf-desc-ch" style="text-align:right">
                            Insurance Zip</div></td>
                            <td align="left">
                                <asp:TextBox ID="txtSecInsZip" runat="server" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">
                                <div class="td-PatientInfo-lf-desc-ch">
                            Insurance Address</div></td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="txtSecInsAddress" runat="server" TextMode="MultiLine" 
                                    Width="495px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">
                                <div class="td-PatientInfo-lf-desc-ch">
                            Insurance State</div></td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="txtSecInsStateExtd" runat="server" CssClass="text-box" 
                                    visible="False" ReadOnly="True" ></asp:TextBox>
                                <extddl:ExtendedDropDownList ID="extdlSecInsState" runat="server" Width="90%" Connection_Key="Connection_String"
                                        Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---"
                                        OldText="" StausText="False" Enabled="False"></extddl:ExtendedDropDownList></td>
                        </tr>
                        
                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tablecellLabel" style="background-color: #DCDCDC">
                                <asp:Label ID="Label22" runat="server" Font-Bold="True" 
                                    Text="SECONDARY POLICY HOLDER DETAILS" Font-Size="Small"></asp:Label>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tablecellLabel">
            <table>     
                        <tr>
                            <td class="tablecellLabel">
                            <div class="td-PatientInfo-lf-desc-ch">
                            Policy Holder</div>
                                
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtSecPolicyHolder" runat="server" Width="495px" 
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">
                                <div class="td-PatientInfo-lf-desc-ch">
                            City</div></td>
                            <td align="left">
                                <asp:TextBox ID="txtSecPolicyCity" runat="server" ReadOnly="True"></asp:TextBox></td>
                            <td class="tablecellLabel">
                                <div class="td-PatientInfo-lf-desc-ch" style="text-align:right">
                            Zip</div></td>
                            <td align="left">
                                <asp:TextBox ID="txtSecPolicyZip" runat="server" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">
                            <div class="td-PatientInfo-lf-desc-ch">
                            Address</div>
                                
                            </td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="txtSecPolicyAddr" runat="server" TextMode="MultiLine" 
                                    Width="495px" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">
                                   <div class="td-PatientInfo-lf-desc-ch">
                            Phone</div>                         
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSecPolicyPh" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tablecellLabel">
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="tablecellLabel">
                             <div class="td-PatientInfo-lf-desc-ch">
                            State</div>                                
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtSecPolicyState" runat="server" CssClass="text-box" 
                                    visible="False" ReadOnly="True" ></asp:TextBox>
                                    <extddl:ExtendedDropDownList ID="extdlSecPolicyState" runat="server" 
                                    Width="90%" Connection_Key="Connection_String"
                                        Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---"
                                        OldText="" StausText="False" Enabled="False"></extddl:ExtendedDropDownList>
                            </td>
                        </tr>
                        <tr align="center">
                            <td class="tablecellLabel">
                            <div class="td-PatientInfo-lf-desc-ch">
                            Relation with Patient</div> 
                                
                            </td>
                            <td colspan="3" align="left">
                                <asp:RadioButtonList id="rdlSecPolicyRelation" runat="server" 
                                        RepeatDirection="Horizontal" Enabled="False" >
                                    <asp:ListItem Value="1" Text="Self" ></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Spous"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Child"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Other"></asp:ListItem>
                                    </asp:RadioButtonList>
                            </td>
                        </tr>
                        
                    </table>
                            </td>
                        </tr>
                    </table>
                    
                </div>
            </td>
        </tr>


            <tr align="center">
                <td width="100%">
                    
                </td>
            </tr>
            <tr>
            <td align="center">
                <asp:RadioButtonList ID="rdlBill" runat="server" Font-Bold="True" 
                    RepeatDirection="Horizontal" AutoPostBack ="false">
                    <asp:ListItem Value="Primary" Selected="True">Generate Bill with Primary</asp:ListItem>
                    <asp:ListItem Value="Secondary">Generate Bill with Secondary</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            </tr>
            <tr>
            <td align="center">
            <asp:RadioButtonList id="rdlbillType" runat="server"
                                        RepeatDirection="Horizontal" >
                                    <asp:ListItem Value="WC000000000000000002" Text="NF3" Selected="True" ></asp:ListItem>                                    
                                    <asp:ListItem Value="WC000000000000000001_1" Text="Doctor's Initial Report"></asp:ListItem>
                                    <asp:ListItem Value="WC000000000000000001_2" Text="Doctor's Progress Report"></asp:ListItem>
                                    <asp:ListItem Value="WC000000000000000001_3" Text="Doctor's Report of MMI"></asp:ListItem>
                                    <asp:ListItem Value="WC000000000000000001_4" Text="OTPT Report"></asp:ListItem>
                                    <asp:ListItem Value="WC000000000000000001_5" Text="Psychologist Report"></asp:ListItem>
                                    <asp:ListItem Value="WC000000000000000001_6" Text="C-4 AMR"></asp:ListItem>
                                    <asp:ListItem Value="WC000000000000000003" Text="PRIVATE"></asp:ListItem>
                                    <asp:ListItem Value="WC000000000000000005" Text="UB4"></asp:ListItem>
                                    <asp:ListItem Value="WC000000000000000004" Text="LIEN"></asp:ListItem>
                                    </asp:RadioButtonList>
            </td>
            </tr>
            <tr>
            <td align="center" class="ContentLabel">
            <table>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnGenerateBill" runat="server" Text="Generate Bill" Width="100px" onclick="btnGenerateBill_Click" ></dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton ID="btnGenerateSecondBill" runat="server" Text="Generate Second Bill" Width="150px" Visible="false" onclick="btnGenerateSecondBill_Click" ></dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton ID="btnRegenerateBill" runat="server" Text="Re-Generate Bill" Width="116px" Visible="false" onclick="btnRegenerateBill_Click" ></dx:ASPxButton>
                </td>
            </tr>
            </table>               
                
            </td>
            </tr>
            
        </table>
    
    </div>
    <asp:HiddenField ID="hdBillNumber" runat="server" />
    <asp:HiddenField ID="hdnSecondExist" runat="server" />
    </form>
</body>
</html>
