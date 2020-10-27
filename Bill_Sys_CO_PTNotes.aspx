<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CO_PTNotes.aspx.cs" Inherits="Bill_Sys_CO_PTNotes" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table class="ContentTable">
        <tr>
            <td class="TDPart">
                <table width="100%">
                    <tr>
                        <td align="left" colspan="4">
                            <asp:Label ID="lblHeading" runat="server" Text="PT Progress Notes" style="font-weight:bold;"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td align="left" colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 19%; font-weight:bold; height: 16px;">
                            <asp:Label ID="Label1" runat="server" Text="Name of the Patient"></asp:Label></td>
                        <td align="left" style="width: 25%; font-weight:bold; height: 16px;"  class="lbl">
                            <asp:TextBox ID="txtPatientName" runat="server"  BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" style="float:left" Font-Size="X-Small" ></asp:TextBox></td>
                             <td align="left" style="width: 12%; font-weight:bold; height: 16px;" >
                            <asp:Label ID="lblCaseno" runat="server" Text="Case #"></asp:Label></td>
                        <td align="left" style="width: 26%; height: 16px;" class="lbl">
                            <asp:TextBox ID="txtCaseNo" runat="server"  BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" style="float:left" Font-Size="X-Small"></asp:TextBox></td>
                       <%-- <td align="left" style="width: 12%">
                            <asp:Label ID="lblPrecautions" runat="server" Text="Precautions"></asp:Label></td>
                        <td align="left" style="width: 26%">
                            <asp:TextBox ID="txtPrecautions" runat="server"></asp:TextBox></td>--%>
                    </tr>
                    
                       <tr>
                        <td align="left" style="width: 19%; font-weight:bold; height: 16px;">
                            <asp:Label ID="lblDateofAccident" runat="server" Text="Date of Accident"></asp:Label></td>
                        <td align="left" style="width: 25%; height: 16px;" class="lbl">
                            <asp:TextBox ID="txtDAO" runat="server"  BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" style="float:left" Font-Size="X-Small"  ></asp:TextBox></td>
                             <td align="left" style="width: 12%; font-weight:bold; height: 16px;">
                            <asp:Label ID="lblInsCmp" runat="server" Text="Insurance Company"></asp:Label></td>
                        <td align="left" style="width: 26%; height: 16px;" class="lbl">
                            <asp:TextBox ID="txtInsCmp" runat="server" Width="100%"  BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" style="float:left" Font-Size="X-Small"></asp:TextBox></td>
                      
                    </tr>
                    
                       <tr>
                       <td align="left" style="width: 23%;font-weight:bold;">
                            <asp:Label ID="lblClaimNumber" runat="server" Text="Claim Number"></asp:Label></td>
                        <td align="left" class="lbl">
                            <asp:TextBox ID="txtClaimNumber" runat="server" Width="149px"  BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" style="float:left" Font-Size="X-Small"></asp:TextBox></td>
                           
                            
                        <td align="left" style="width: 19%; font-weight:bold;">
                            <asp:Label ID="Label4" runat="server" Text="Date "></asp:Label></td>
                        <td align="left" style="width: 25%;" class="lbl"> 
                            <asp:TextBox ID="txtDate" runat="server" 
                                Width="35%" MaxLength="10"  BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" style="float:left" Font-Size="X-Small"></asp:TextBox>
                            
                        </td>
                        
                       
                    </tr>
                    <tr>
                    <td colspan="4" width="100%">&nbsp;</td>
                    </tr>
                   
                </table>
                <table width="100%">
                  
                </table>
                <table width="100%">
                    
                </table>
                <table width="100%">
                    <tbody>
                     <tr>
                        <td align="left" style="width: 23%">
                            <asp:Label ID="lblPcomplaints" runat="server" Text="Patient complaints" Font-Bold="True"></asp:Label></td>
                        <td align="left">
                            <asp:TextBox ID="txtPatientcomplaints" runat="server" Width="149px"></asp:TextBox></td>
                            <td align="left" style="width: 12%">
                            
                            <asp:Label ID="lblPrecautions" runat="server" Text="Precautions" Font-Bold="True"></asp:Label></td>
                        <td align="left" style="width: 26%">
                            <asp:TextBox ID="txtPrecautions" runat="server"></asp:TextBox></td>
                    </tr>
                           <tr valign="top">
                            <td align="left" style="width: 23%; height: 20px;" valign="top">
                                <asp:Label ID="Label2" runat="server" Text="Treatment" Font-Bold="True"></asp:Label></td>
                            <td align="left" style="width: 26%; height: 20px;">
                                <asp:CheckBox ID="chkInitialVisite" runat="server" Text="Initial Visit-PT" Width="100px"></asp:CheckBox>
                            </td>
                            <td align="left" style="width: 26%; height: 20px;">
                                <asp:CheckBox ID="ChkNewPatient" runat="server" Text="Initial Evaluation of New patient "
                                    Width="191px"></asp:CheckBox></td>
                            <td align="left" style="width: 26%; height: 20px;">
                                <asp:CheckBox ID="chkPTEval" runat="server" Text="PT Eval" Width="171px">
                                </asp:CheckBox></td>
                        </tr>
                    
                        <tr valign="top">
                            <td align="left" style="width: 23%; height: 20px;" valign="top">
                                </td>
                            <td align="left" style="width: 26%; height: 20px;">
                                <asp:CheckBox ID="chkColdpack" runat="server" Text="Cold pack" Width="100px"></asp:CheckBox>
                            </td>
                            <td align="left" style="width: 26%; height: 20px;">
                                <asp:CheckBox ID="chkElectricalMS" runat="server" Text="Electrical Muscle Stimulation"
                                    Width="191px"></asp:CheckBox></td>
                            <td align="left" style="width: 26%; height: 20px;">
                                <asp:CheckBox ID="chkTherapeuticEx" runat="server" Text="Therapeutic Exercise" Width="171px">
                                </asp:CheckBox></td>
                        </tr>
                        <tr>
                            <td style="width: 21%">
                            </td>
                            <td style="width: 23%" align="left">
                                <asp:CheckBox ID="chkHotpack" runat="server" Text="Hot pack" Width="85px"></asp:CheckBox>
                            </td>
                            <td style="width: 23%" align="left">
                                <asp:CheckBox ID="chkUltraSound" runat="server" Text="Ultra Sound" Width="110px"></asp:CheckBox></td>
                            <td style="width: 23%" align="left">
                                <asp:CheckBox ID="chkMyofascialR" runat="server" Text="Myofascial Release" Width="159px">
                                </asp:CheckBox>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 21%">
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="chkTENS" runat="server" Text="TENS" Width="68px"></asp:CheckBox></td>
                            <td align="left">
                                <asp:CheckBox ID="chkTherapeuticM" runat="server" Text="Therapeutic Massage" Width="172px">
                                </asp:CheckBox></td>
                            <td align="left">
                                <asp:CheckBox ID="chkParaffin" runat="server" Text="Paraffin Bath" Width="115px"></asp:CheckBox></td>
                        </tr>
                        
                                          <tr>
                            <td style="width: 21%">
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="chkBalanceCoord" runat="server" Text="Balance Coord Postur " Width="139px"></asp:CheckBox></td>
                            <td align="left">
                                <asp:CheckBox ID="ChkRemovalofTissues" runat="server" Text="Removal of devitalized tissues  " Width="172px">
                                </asp:CheckBox></td>
                            <td align="left">
                                
                        </tr>
                    </tbody>
                </table>
                <table style="width: 100%; height: 268px">
                    <tbody>
                        <tr valign="top">
                            <td align="left" style="width: 23%">
                                <asp:Label ID="lblObjective" runat="server" Text="Objective" Font-Bold="True"></asp:Label></td>
                            <td align="left" style="width: 627px">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2" style="height: 20px">
                                                <asp:CheckBox ID="chkPatientstatescondition" runat="server" Width="250px" Text="Patient states condition is the same">
                                                </asp:CheckBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:CheckBox ID="chkPatientstateslittle" runat="server" Width="278px" Text="Patient states little improvement in condition">
                                                </asp:CheckBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBox ID="chkPatientstatesmuch" runat="server" Width="302px" Text="Patient states much improvement in condition">
                                                </asp:CheckBox></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td align="left">
                                <asp:Label ID="lblAssessment" runat="server" Text="Assessment" Font-Bold="True"></asp:Label></td>
                            <td align="left" style="width: 627px">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:CheckBox ID="chkPatienttolerated" runat="server" Width="221px" Text="Patient tolerated maximum level">
                                                </asp:CheckBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:CheckBox ID="chkOtherComments" runat="server" Width="143px" Text="Other comments">
                                                </asp:CheckBox>
                                                <asp:TextBox ID="txtOthercomments" runat="server" Width="220px" Text=""></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td align="left">
                                <asp:Label ID="lblPlan" runat="server" Text="Plan" Font-Bold="True"></asp:Label></td>
                            <td align="left" style="width: 627px">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:CheckBox ID="chkProgresstherapy" runat="server" Width="302px" Text="Continue/Progress therapy as prescribed">
                                                </asp:CheckBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:CheckBox ID="chkPlanOthercomments" runat="server" Width="136px" Text="Other comments">
                                                </asp:CheckBox>
                                                <asp:TextBox ID="txtPlanOthercomments" runat="server" Width="223px" Text=""></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:TextBox ID="txtCompanyName" runat="server" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <%-- <td align="left" style="height: 10px">
                                <asp:Label ID="lblTSignature" runat="server" Text="Therapist's Signature"></asp:Label></td>--%>
                            <td align="left" style="width: 627px; height: 10px">
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td align="center">
                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                            <asp:Button ID="css_btnSave" runat="server" Text="Save" OnClick="css_btnSave_Click"
                                CssClass="Buttons" /> 
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                                CssClass="Buttons" /> 
                            <asp:TextBox ID="txtEventID" runat="Server" Visible="false" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtCaseID" runat="Server" Visible="false" Width="10px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
