<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_Physical_Theraphy_Aqua.aspx.cs" Inherits="Bill_Sys_Physical_Theraphy_Aqua" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


        <table width="100%" class ="TDPart">
        <tr>
            <td style="height: 26px; width: 137px;" class="lbl" valign="top" >
                Patient Name</td>
            <td style="height: 26px; width: 47px;">
                <asp:TextBox ID="txtPatientName" runat="server" ReadOnly="True" Width="212px"></asp:TextBox>
                &nbsp; &nbsp;
             </td>  
           
        
            <td style="height: 26px; width: 48px;" class="lbl" valign="top">
                DOA</td>
            <td style="height: 26px; width: 546px;" valign="top">
                <asp:TextBox ID="txtDOA" runat="server" ReadOnly="True" Width="109px"></asp:TextBox>
                &nbsp; &nbsp;
             </td>  
           
        </tr>
        <tr>
            <td style="height: 21px" colspan="2">
            <table width="100%">
                <tr>
                <td class="lbl" style="width: 127px">
                    TreatementDate
                </td>
                <%--<td>
                    PatientSign
                </td>--%>
                <td class="lbl">
                    ProgressLevel
                </td>
                </tr>
                  <tr>
                <td valign="top" style="width: 127px">
                    &nbsp;<asp:TextBox ID="TXT_TREATMENT" runat="server" Width="117px"></asp:TextBox></td>
             <%--   <td>
                    PatientSign
                </td>--%>
                <td>
                    <table width="100%">
                    <tr>
                        <td>
                            <asp:CheckBox ID="CHK_PROGRESS_1" runat="server" Text="1" />                            
                        </td>
                        <td>
                            <asp:CheckBox ID="CHK_PROGRESS_2" runat="server" Text="2" />                            
                        </td>
                        <td>
                            <asp:CheckBox ID="CHK_PROGRESS_3" runat="server" Text="3" />                            
                        </td>
                        <td>
                            <asp:CheckBox ID="CHK_PROGRESS_4" runat="server" Text="4" />                            
                        </td>
                        <td>
                            <asp:CheckBox ID="CHK_PROGRESS_5" runat="server" Text="5" />                            
                        </td>
                        <td>
                            <asp:CheckBox ID="CHK_PROGRESS_6" runat="server" Text="6" />                            
                        </td>
                        <td>
                            <asp:CheckBox ID="CHK_PROGRESS_7" runat="server" Text="7" />                            
                        </td>
                    </tr>
                    </table>
                </td>
                </tr>
            </table>
           
            </td>
        </tr>
            <tr>
                <td align="Center" style="width: 137px">
                    <asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click" Width="62px" CssClass="Buttons" />
                </td>
                <td style="width: 47px">
                  <asp:TextBox ID="TXT_I_EVENT" runat="server" Width="81px" Visible="FALSE"></asp:TextBox>                
                </td>
            </tr>
        
        </table>
   </asp:Content>
