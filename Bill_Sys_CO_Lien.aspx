<%@ Page Language="C#"  AutoEventWireup="true"  MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_CO_Lien.aspx.cs" Inherits="Bill_Sys_CO_Lien" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table class="ContentTable" width="100%">
        <tr>
            <td class="TDPart" align="center" valign="top" style="height: 157px">
                <table width="100%">
                    <tr>
                        <td align="center" style="height: 18px">
                            <asp:Label ID="lblFormHeader" runat="server" Text="AUTHORIZATION  OF  DIRECT  PAYMENTS  AND  LIEN" Style="font-weight: bold;" Width="401px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="height: 18px">
                        </td>
                    </tr>
                </table>
                
                <table width="100%">
                
                   
                        <tr>
                            <td align="right" valign="top" style="width: 213px">
                                <asp:Label ID="lbl_Patient_Name" runat="server" Text="Patient Name  " Width="144px"></asp:Label></td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="TXT_PATIENT_NAME" runat="server"  Visible="false" Width="1px"></asp:TextBox>
                                <asp:Label ID="TXT_PATIENT_NAME1" runat="server" ></asp:Label>
                                </td>
                        </tr>
                        <tr>
                        <td align="right" style="width: 213px" >
                            <asp:Label ID="Label1" runat="server" Text="Accident Date "  ></asp:Label></td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="TXT_ACCIDENT_INVOLVED" runat="server" Width="1px" Visible="false"></asp:TextBox>
                            <asp:Label ID="TXT_ACCIDENT_INVOLVED1" runat="server" Width="94px"></asp:Label></td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="height: 23px; width: 213px;">
                            <asp:TextBox ID="TXT_I_EVENT" runat="server" Visible="false" Height="5px" Width="102px">1</asp:TextBox></td>
                            <td align="left" valign="middle" style="width: 331px; height: 23px">
                            <asp:Button ID="css_btnSave" runat="server" CssClass="Buttons" OnClick="css_btnSave_Click" Text="Save" />&nbsp;
                            <asp:Button ID="btnSavePrint" runat="server" CssClass="Buttons" Text="Save & Print"  OnClick="btnSavePrint_Click"/>
                                <asp:TextBox ID="txtCompanyID" runat="server" Visible="false" Width="53px"></asp:TextBox><asp:TextBox ID="txtEventID" runat="Server" Visible="false" Width="10px"></asp:TextBox><asp:TextBox ID="txtCaseID" runat="Server" Visible="false" Width="10px"></asp:TextBox></td>
                        </tr>            
                
                </table>
            </td>
        </tr>
        
    </table>
     </asp:Content>


