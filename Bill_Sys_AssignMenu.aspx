<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Bill_Sys_AssignMenu.aspx.cs" Inherits="Bill_Sys_AssignMenu" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" src="validation.js"></script>
<table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align:top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftTop">
                        </td>
                        <td class="CenterTop">
                        </td>
                        <td class="RightTop">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                
                               
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                        
                                        <tr>
                                                <td class="ContentLabel" style="text-align:center; height:25px;" colspan="4" >
                                                <asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>
                                                <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                                </td> 
                                                </tr> 
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Bill Status:</td>
                                                <td style="width: 35%">
                                                    <extddl:ExtendedDropDownList ID="extddlUserRole" runat="server"  Width="250px" Connection_Key="Connection_String" Flag_Key_Value="GET_USER_ROLE_LIST" Procedure_Name="SP_MST_USER_ROLES" Selected_Text="---SELECT---"   Maintain_State="true" Visible=false />
                   <asp:DropDownList ID="ddlUserRole" runat="server" Width = "250px" OnSelectedIndexChanged="ddlUserRole_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>                   
                    <asp:RequiredFieldValidator ID="rfvddlUserRole" runat="server" ErrorMessage="Select Role" ControlToValidate="ddlUserRole"></asp:RequiredFieldValidator>
                    </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                         
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                  <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" cssclass="Buttons"/>
                                                   </td>
                                            </tr>
                                        </table>
                                    
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                    
                                          <asp:TreeView runat="Server" OnTreeNodePopulate="Node_Populate" ID="tvwmenu" OnSelectedNodeChanged="tvwmenu_SelectedNodeChanged" OnTreeNodeCheckChanged="tvwmenu_TreeNodeCheckChanged">
                            <Nodes>
                                  <asp:TreeNode Text="Menu" PopulateOnDemand="true" Value="0"></asp:TreeNode>
                                  
                            </Nodes>
                        </asp:TreeView>
                                    </td>
                                </tr>
                            </table>
    
</td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>


